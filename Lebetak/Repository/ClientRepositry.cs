using AutoMapper;
using Lebetak.DTOs.Report;
using Lebetak.Models;

namespace Lebetak.Repository
{
    public class ClientRepositry : BaseRepo<Client>
    {
        private LebetakContext context;
        public ClientRepositry(LebetakContext _context) : base(_context)
        {
            context = _context;
        }
        public new Client GetById(string id)
        {
            return context.Clients.Find(id);
        }
        public bool ReportToWorker(string clientId, Report report)
        {

            var res = context.Reports.Add(report);
            if (res != null) {
                return true;
            }
                return false;
        }
    }
}
