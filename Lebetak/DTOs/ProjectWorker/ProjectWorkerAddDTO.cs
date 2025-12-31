using System.ComponentModel.DataAnnotations;

namespace Lebetak.DTOs.ProjectWorker
{
    public class ProjectWorkerAddDTO
    {
        public string TitleOfProject { get; set; }
        public string DescriptionOfProject { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
