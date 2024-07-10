using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace K_Bridge.Models
{
    public class GlobalChat
    {
        [BindNever]
        public int ID { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        public DateTime SendAt { get; set; } = DateTime.Now;
    }
}