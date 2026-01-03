namespace Lebetak.DTOs
{
    public class MessageViewDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsFromClient { get; set; }

    }
}
