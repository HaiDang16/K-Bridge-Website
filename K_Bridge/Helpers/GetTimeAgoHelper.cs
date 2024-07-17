namespace K_Bridge.Helpers
{
    public static class GetTimeAgoHelper
    {
        public static string GetTimeAgo(DateTime dateTime)
        {
            var timeSpan = DateTime.UtcNow - dateTime.ToUniversalTime();

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                return $"{timeSpan.Seconds} giây trước";
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                return $"{timeSpan.Minutes} phút trước";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                return $"{timeSpan.Hours} giờ trước";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                return $"{timeSpan.Days} ngày trước";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                return $"{timeSpan.Days / 30} tháng trước";
            }
            else
            {
                return $"{timeSpan.Days / 365} năm trước";
            }
        }
    }
}
