namespace Lebetak.DTOs
{
    public class ProjectProfileDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TotalReacts { get; set; }
        public List<string>? Images { get; set; }
    }
}
