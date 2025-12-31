using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        //Navigation Properties
        public virtual Question? question { get; set; }
        public virtual ICollection<AttAnswer>? attAnswers { get; set; }
    }
}
