using Lebetak.Models;

namespace Lebetak.Repository
{
    public class ServiceAnswerRepositry : BaseRepo<ServiceAnswer>
    {
        public ServiceAnswerRepositry(LebetakContext _context) : base(_context)
        {
        }
    }
}
