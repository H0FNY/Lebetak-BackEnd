using Lebetak.Models;

namespace Lebetak.Repository
{
    public class CategoryRepository : BaseRepo<Category>
    {
        public CategoryRepository(LebetakContext _context) : base(_context) 
        {
        }

    }
}
