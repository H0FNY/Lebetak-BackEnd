namespace Lebetak.Models
{
    public class ProposalNotification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int PostId { get; set; }
    }
}
