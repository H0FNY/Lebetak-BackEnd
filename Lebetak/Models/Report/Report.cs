namespace Lebetak.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string WorkerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual Client? Client { get; set; }
        public virtual Worker? Worker { get; set; }
    }
}
