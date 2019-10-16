using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class MessageDTO
    {
        public int id { get; set; }
        public int authorId { get; set; }
        public int bookId { get; set; }
        public string message { get; set; }
        public int rating { get; set; }
    }
}
