using Lebetak.Models;

namespace Lebetak.Repository
{
    public class PostStatusRepositry : BaseRepo<PostStatus>
    {
        public PostStatusRepositry(LebetakContext _context) : base(_context)
        {
        }

    }
}
