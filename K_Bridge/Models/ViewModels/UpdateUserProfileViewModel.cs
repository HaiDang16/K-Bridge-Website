using System.ComponentModel.DataAnnotations;

namespace K_Bridge.Models.ViewModels
{
    public class UpdateUserProfileViewModel
    {
        public string? Biography { get; set; }

        public IFormFile? AvatarFile { get; set; }

        public string? ProfileColor { get; set; }
    }
}
