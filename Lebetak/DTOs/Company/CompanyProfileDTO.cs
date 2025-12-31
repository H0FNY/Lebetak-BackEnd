namespace Lebetak.DTOs
{
    public class CompanyProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Panner { get; set; }
        public string Description { get; set; }
        public bool IsVerified { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<ProjectCardDTO>? Projects { get; set; }

    }
}
