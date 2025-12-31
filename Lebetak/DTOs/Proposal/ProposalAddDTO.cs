using Lebetak.Models;

namespace Lebetak.DTOs
{
    public class ProposalAddDTO
    {
        public string Description { get; set; }
        //public string WorkerId { get; set; }
        public double Price { get; set; }
        public int PostId { get; set; }
        public List<IFormFile>? Files { get; set; }

    }
}
