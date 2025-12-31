using Lebetak.Models;

namespace Lebetak.Repository
{
    public class PostRepositry : BaseRepo<Post>
    {
        public PostRepositry(LebetakContext _context) : base(_context)
        {
        }
    }
}
