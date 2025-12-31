
namespace Lebetak.DTOs
{
    public class ServiceAddDTO
    {
        public string Description { get; set; }
        public DateTime PreferredTime { get; set; }
        public int CompanyId { get; set; }
        public List<ServiceAnswerDTO> Answers { get; set; }
        public List<IFormFile>? Images { get; set; }


    }
}
