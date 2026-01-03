namespace Lebetak.Models.ChatModel
{
    public class Message
    {
        // Abdelrhman Edit it
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsFromClient { get; set; }
        public bool IsReaded { get; set; }

        // relation 
        public int chatId { get; set; }
        public virtual Chat Chat { get; set; }

    }
}
