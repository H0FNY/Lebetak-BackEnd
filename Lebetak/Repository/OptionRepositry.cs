using Lebetak.Models;

namespace Lebetak.Repository
{
    public class OptionRepositry : BaseRepo<Option>
    {
        public OptionRepositry(LebetakContext _context) : base(_context)
        {
        }
    }
}
