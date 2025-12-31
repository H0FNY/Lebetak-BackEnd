using Lebetak.Models;

namespace Lebetak.Repository
{
    public class QuestionRepositry : BaseRepo<Question>
    {
        public QuestionRepositry(LebetakContext _context) : base(_context)
        {

        }
    }
}
