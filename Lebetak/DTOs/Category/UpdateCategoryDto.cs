namespace Lebetak.DTOs
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Icon { get; set; }
    }
}
