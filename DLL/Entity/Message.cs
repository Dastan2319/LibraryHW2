using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entity
{
    public class Message
    {
        public int id { get; set; }
        public int authorId { get; set; }
        public int bookId { get; set; }
        public string message { get; set; }
    }
}
