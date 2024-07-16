using System.Security.Cryptography;
using System.Text;

namespace K_Bridge.Helpers
{
    public static class EncryptIDHelper
    {
        private static readonly string Key = "YourSecretKey123";

        public static string EncryptID(int id)
        {
            string idString = id.ToString();
            return Encrypt(idString, Key);
        }
        public static int? DecryptID(string encryptedId)
        {
            try
            {
                string decryptedString = Decrypt(encryptedId, Key);
                if (int.TryParse(decryptedString, out int id))
                {
                    return id;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        private static string Encrypt(string plainText, string password)
        {
            byte[] encryptedBytes;
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = CreateKey(password);
                aes.GenerateIV();

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainBytes, 0, plainBytes.Length);
                        cs.FlushFinalBlock();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        private static string Decrypt(string encryptedText, string password)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = CreateKey(password);

                using (MemoryStream ms = new MemoryStream(encryptedBytes))
                {
                    byte[] iv = new byte[16];
                    ms.Read(iv, 0, 16);
                    aes.IV = iv;

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static byte[] CreateKey(string password, int keyBytes = 32)
        {
            byte[] salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int iterations = 300;
            var keyGenerator = new Rfc2898DeriveBytes(password, salt, iterations);
            return keyGenerator.GetBytes(keyBytes);
        }
    }
}
