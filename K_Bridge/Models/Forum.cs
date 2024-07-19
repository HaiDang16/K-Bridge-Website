using System.ComponentModel.DataAnnotations;

namespace K_Bridge.Models
{
    public class Forum : BaseModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên diễn đàn")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? IconLink { get; set; }
        public string? TagName { get; set; }
/*        public string? IconAdminLink {  get; set; }
*/


        public ICollection<Topic> Topics { get; set; } = new List<Topic>();

    }
}