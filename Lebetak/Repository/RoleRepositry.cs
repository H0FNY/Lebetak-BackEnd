using Lebetak.Models;
using Microsoft.AspNetCore.Identity;

namespace Lebetak.Repository
{
    public class RoleRepository : BaseRepo<IdentityRole>
    {
        public RoleRepository(LebetakContext _context) : base(_context)
        {
        }
    }
}
