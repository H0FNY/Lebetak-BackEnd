namespace Lebetak.Models
{
    public class ServiceAnswer
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }

        // Navigation Properties
        public virtual Service? Service { get; set; }
        public virtual Question? Question { get; set; }
        public virtual Option? Option { get; set; }
    }
}

