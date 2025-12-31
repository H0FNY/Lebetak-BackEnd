using Lebetak.Models.ChatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class Client
    {
        public string UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<Service>? Services { get; set; }

        // Relations
        public virtual ICollection<Chat>? Chats { get; set; }
        public virtual ICollection<Report>? Reports { get; set; }

    }
}   
