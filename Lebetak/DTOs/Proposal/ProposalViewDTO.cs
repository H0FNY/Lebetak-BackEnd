namespace Lebetak.DTOs.Proposal
{
    public class ProposalViewDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string WorkerId { get; set; }
        public string WorkerName { get; set; }
        public string WorkerPicUrl { get; set; }
        public string Description { get; set; }
        public DateTime Created_At { get; set; }
        public double Price { get; set; }
        public string StatusName { get; set; }
        public List<string> Images { get; set; }
    }
}
