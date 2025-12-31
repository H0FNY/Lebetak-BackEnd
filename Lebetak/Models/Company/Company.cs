namespace Lebetak.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Panner { get; set; }
        public string Description { get; set; }
        public bool IsVerified { get; set; }
        public string OwnerId { get; set; }
        public int CategoryId { get; set; }

        //Navigation Property 
        public virtual Owner Owner { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ComponyPhones>? ComponyPhones { get; set; }
        public virtual ICollection<ProjectCompany>? Projects { get; set; }

    }
}
