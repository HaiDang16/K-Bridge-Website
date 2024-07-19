using K_Bridge.Models;
using Microsoft.EntityFrameworkCore;

namespace K_Bridge.Services
{
    public class CodeGenerationService
    {
        private readonly KBridgeDbContext _context;

        public CodeGenerationService(KBridgeDbContext context)
        {
            _context = context;
        }

        public string GenerateNewCode<T>(string prefix) where T : class
        {
            string newCode = "";
            var dbSet = _context.Set<T>();
            int count = dbSet.Count();

            if (count == 0)
            {
                newCode = $"{prefix}001";
            }
            else
            {
                var lastItem = dbSet.OrderByDescending(GetCodeProperty<T>()).FirstOrDefault();

                if (lastItem == null)
                {
                    newCode = $"{prefix}001";
                }
                else
                {
                    string lastCode = GetCodePropertyValue(lastItem);
                    if (lastCode != null)
                    {
                        int lastCodeNum = int.Parse(lastCode.Substring(prefix.Length));
                        int newCodeNum = lastCodeNum + 1;
                        newCode = $"{prefix}{newCodeNum:D3}";
                    }
                    else
                    {
                        // Handle case where lastCode is null or empty
                        newCode = $"{prefix}001";
                    }
                }
            }
            return newCode;
        }

        private Func<T, string> GetCodeProperty<T>()
        {
            var entityType = _context.Model.FindEntityType(typeof(T));
            var propertyName = "Code"; // Change this to match your actual property name
            var property = entityType?.FindProperty(propertyName);
            var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), "x");
            var body = System.Linq.Expressions.Expression.Property(parameter, property.PropertyInfo);
            var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, string>>(body, parameter);
            return lambda.Compile();
        }

        private string GetCodePropertyValue<T>(T entity)
        {
            var getCodeProperty = GetCodeProperty<T>();
            return getCodeProperty(entity);
        }
    }
}
