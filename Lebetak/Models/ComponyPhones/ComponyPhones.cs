namespace Lebetak.Models
{
    public class ComponyPhones
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string PhoneNumber { get; set; }
        public virtual Company Company { get; set; }
    }
}
