using Lebetak.Models;

namespace Lebetak.Repository
{
    public class UserRepositry : BaseRepo<User>
    {
        public UserRepositry(LebetakContext _context) : base(_context)
        {
        }
    }
}
