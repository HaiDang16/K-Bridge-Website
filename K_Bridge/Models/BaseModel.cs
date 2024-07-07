namespace K_Bridge.Models
{
    public class BaseModel
    {
        public int ID { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
    }
}
