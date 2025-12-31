namespace Lebetak.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }

        // Navigation Properties
        public virtual Category? Category { get; set; }
        //public virtual Answer? answer { get; set; }
        public virtual ICollection<Option>? options { get; set; }
    }
}
