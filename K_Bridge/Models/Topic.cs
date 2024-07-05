namespace K_Bridge.Models
{
    public class Topic
    {
        public long? ID { get; set; }
        public string Name { get; set; } = String.Empty;
        public long CateId { get; set; }
        public int PostCount { get; set; } = 0;
        public string LinkIcon { get; set; } = String.Empty;
        public bool StateTopic { get; set; } = true;
        public DateTime JoinDate { get; set; }
    }
}
