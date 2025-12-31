namespace Lebetak.DTOs.Question
{
    public class QuestionViewDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<OptionViewDTO> answers { get; set; }
    }
}
