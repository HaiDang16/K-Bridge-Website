using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace K_Bridge.Models
{
    public class BaseModel
    {
        [BindNever]
        public int ID { get; set; }

        [BindNever]
        public string? Code { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
