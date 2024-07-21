using System.ComponentModel.DataAnnotations;

namespace K_Bridge.Models.ViewModels
{
    public class UpdatePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
