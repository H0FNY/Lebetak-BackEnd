using Lebetak.DTOs.Worker;
using Lebetak.Models;
using Lebetak.Profiles;

namespace Lebetak.Repository
{
    public class WorkerRepositry : BaseRepo<Worker>
    {
        private LebetakContext context;
        public WorkerRepositry(LebetakContext _context) : base(_context)
        {
            context = _context;
        }
        public new Worker GetById(string id)
        {
            return context.Workers.Find(id);
        }
        public IQueryable<Worker> SearchWorkerByCategoryID(int CategoryID){
            return context.Workers.Where(w=>w.CategoryId == CategoryID);
        }
        public IQueryable<Worker> SearchWorkerByHourlyPrice(int lPrice,int hPrice)
        {
            return context.Workers.Where(w => w.HourlyPrice > lPrice && w.HourlyPrice < hPrice);
        }
    }
}
