namespace Lebetak.DTOs.Service
{
    public class ServiceProfileDTO
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientPic { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PreferredTime { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPic { get; set; }
        public List<string> Images { get; set; }
        public List<QuestionAndAnswerDTO> answers { get; set; }
    }
}
