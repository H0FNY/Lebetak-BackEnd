namespace Lebetak.DTOs.Question
{
    public class QuestionAddDTO
    {
        public int CompanyId { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }


    }
}
