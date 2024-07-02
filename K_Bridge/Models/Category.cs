namespace K_Bridge.Models
{
    public class Category
    {
        public long? ID { get; set; }
        public string Name { get; set; } = String.Empty;
        public int TopicCount { get; set; }
        public string LinkIcon { get; set; } = String.Empty;
    }
}
