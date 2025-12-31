namespace Lebetak.DTOs
{
    public class PostViewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Location { get; set; }
        public DateTime CreatedDate { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public decimal BudgetFrom { get; set; }
        public decimal BudgetTo { get; set; }

        public string UrgencyLevel { get; set; }
        public string CategoryName { get; set; }
        public string PostStatus { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientPicUrl { get; set; }

        public List<string>? Images { get; set; }

    }
}
