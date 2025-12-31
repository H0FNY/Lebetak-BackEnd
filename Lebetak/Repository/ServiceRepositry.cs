using Lebetak.Models;

namespace Lebetak.Repository
{
    public class ServiceRepositry : BaseRepo<Service>
    {
        public ServiceRepositry(LebetakContext _context) : base(_context)
        {
        }
    }
}
