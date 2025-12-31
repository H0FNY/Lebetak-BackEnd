namespace Lebetak.DTOs
{
    public class PostCardDTO
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientPicUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal BudgetFrom { get; set; }
        public decimal BudgetTo { get; set; }
        public string UrgencyLevel { get; set; }
        public string CategoryName { get; set; }
        public string PostStatus { get; set; }

    }
}
