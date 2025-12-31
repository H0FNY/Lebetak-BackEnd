using Lebetak.Models;

namespace Lebetak.Repository
{
    public class CompanyRepositry: BaseRepo<Company>
    {
        public CompanyRepositry(LebetakContext _db) : base(_db)
        {
        }
    }
}
