using Lebetak.Models;

namespace Lebetak.Repository
{
    public class WalletRepository : BaseRepo<Wallet>
    {
        public WalletRepository(LebetakContext _context) : base(_context)
        {
        }
    }
}
