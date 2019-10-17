using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MessageViewModel
    {
        public int id { get; set; }
        public int authorId { get; set; }
        public int bookId { get; set; }
        public string message { get; set; }
        public int rating { get; set; }
    }
}