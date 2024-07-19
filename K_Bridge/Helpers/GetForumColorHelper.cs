namespace K_Bridge.Helpers
{
    public static class GetForumColorHelper
    {
        public static string GetForumBackgroundColor(int forumId)
        {
            switch (forumId)
            {
                case 1: //Lập trình
                    return "#FF3434";
                case 2: //Hướng dẫn
                    return "#78B668";
                case 3: //Mẫu báo cáo
                    return "#B66868";
                case 4: //Tài liệu tổng hợp
                    return "#CF63D1";
                case 5: //Bài giảng
                    return "#FF8534";
                case 6: //DRL
                    return "#6563D1";
                default:
                    return "#493422";
            }
        }
        public static string GetForumColor(int forumId)
        {
            switch (forumId)
            {
                default:
                    return "#FFFFFF";
            }
        }
    }
}
