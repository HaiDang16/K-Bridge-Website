using System.ComponentModel.DataAnnotations;

namespace K_Bridge.Models.ViewModels
{
    public class UpdateUsernameViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string NewUserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
