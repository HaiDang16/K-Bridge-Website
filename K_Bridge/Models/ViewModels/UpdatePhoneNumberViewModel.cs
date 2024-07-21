using System.ComponentModel.DataAnnotations;

namespace K_Bridge.Models.ViewModels
{
    public class UpdatePhoneNumberViewModel
    {
        [Required]
        public string NewPhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Username { get; set; }

    }
}
