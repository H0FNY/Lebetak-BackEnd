using System.ComponentModel.DataAnnotations;

namespace Lebetak.DTOs
{
    public class ProjectAddDTO
    {
        [Required]
        public string TitleOfProject { get; set; }
        [Required]
        public string DescriptionOfProject { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
