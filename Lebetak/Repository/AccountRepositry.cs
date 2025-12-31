using Lebetak.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Lebetak.Repository
{
    public class AccountRepositry : BaseRepo<IdentityUser>
    {
        private LebetakContext context;
        public AccountRepositry(LebetakContext _context) : base(_context)
        {
            context = _context;
        }
        public new User GetById(string id)
        {
            return context.User.Find(id);
        }
    }
}
