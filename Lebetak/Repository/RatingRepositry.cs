using Lebetak.Models;

namespace Lebetak.Repository
{
    public class RatingRepositry : BaseRepo<Rating>
    {
        public RatingRepositry(LebetakContext _context) : base(_context)
        {
        }
    }
}
