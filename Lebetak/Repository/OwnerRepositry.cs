using Lebetak.Models;

namespace Lebetak.Repository
{
    public class OwnerRepositry : BaseRepo<Owner>
    {
        private LebetakContext context;
        public OwnerRepositry(LebetakContext _context) : base(_context)
        {
            context = _context;
        }

        public new Owner GetById(string id)
        {
            return context.Owners.Find(id);
        }
    }
}
