namespace Lebetak.DTOs
{
    public class PostAddDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Location { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public decimal BudgetFrom { get; set; }
        public decimal BudgetTo { get; set; }

        public int UrgencyId { get; set; }                  

        public int CategoryId { get; set; }
        
        public List<IFormFile>? Images { get; set; }        
    
    }
}
