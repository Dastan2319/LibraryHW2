using DLL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Repositories;

namespace DLL.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        private Model1 db;

        public MessageRepository(Model1 db)
        {
            this.db = db;
        }
        public void Create(Message item)
        {
            db.Message.Add(item);
        }

        public void Delete(int id)
        {
            Message Message = db.Message.Find(id);
            if (Message != null)
                db.Message.Remove(Message);
        }

        public Message Get(int? id)
        {
            return db.Message.Find(id);
        }

        public IEnumerable<Message> GetAll()
        {
            return db.Message;
        }

        public void Update(Message item)
        {
            db.Message.Where(x => x.id == item.id).FirstOrDefault().message=item.message;
            db.Message.Where(x => x.id == item.id).FirstOrDefault().rating = item.rating;
            db.SaveChanges();
        }
    }
}
