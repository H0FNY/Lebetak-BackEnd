namespace Lebetak.Models
{
    public class Urgency
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
