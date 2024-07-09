using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace K_Bridge.Models
{
    public class BaseModel
    {
        [BindNever]
        public int ID { get; set; }

        [BindNever]
        public string? Code { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
    }
}
