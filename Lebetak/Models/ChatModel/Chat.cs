namespace Lebetak.Models.ChatModel
{
    public class Chat
    {
        public int Id { get; set; }

        // relations 
        public string clientId { get; set; }
        public virtual Client client { get; set; }

        public string WorkerId { get; set; }
        public virtual Worker Worker { get; set; }

        public virtual ICollection<Message> Messages { get; set; }=new List<Message>();
    }
}
