namespace Lebetak.DTOs
{
    public class NotificationViewDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }   // "Chat" أو "Proposal"
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public int? ChatId { get; set; }
        public int? PostId { get; set; }
    }
}
