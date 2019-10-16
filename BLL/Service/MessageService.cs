using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DLL.Entity;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class MessageService: IMessageService
    {
        IUnitOfWork db { get; set; }

        public MessageService(IUnitOfWork uow)
        {
            db = uow;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public MessageDTO GetMessage(int? id)
        {
            if (id != null)
            {
                var book = db.Message.Get(id);
                return new MessageDTO { authorId=book.authorId,bookId=book.bookId, message=book.message };
            }
            else
            {
                return new MessageDTO();
            }
        }

        public IEnumerable<MessageDTO> GetMessage()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Message, MessageDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Message>, List<MessageDTO>>(db.Message.GetAll());
        }

        public void MakeMessage(MessageDTO orderDto)
        {
            Message message = new Message
            {
                authorId=orderDto.authorId,
                message=orderDto.message,
                bookId=orderDto.bookId,
                rating=orderDto.rating
            };
            db.Message.Create(message);
            db.Save();
        }

        public void SaveUpdate(MessageDTO orderDto)
        {
            var msgID =db.Message.GetAll().Where(x => x.authorId == orderDto.authorId & x.bookId == orderDto.bookId).FirstOrDefault();
            if (msgID != null)
            {


                Message message = new Message
                {
                    id = msgID.id,
                    authorId = orderDto.authorId,
                    message = orderDto.message,
                    bookId = orderDto.bookId,
                    rating = orderDto.rating
                };
                db.Message.Update(message);
             
            }
            else
            {
                Message message = new Message
                {
                    authorId = orderDto.authorId,
                    message = orderDto.message,
                    bookId = orderDto.bookId,
                    rating = orderDto.rating
                };
                db.Message.Create(message);
            }
            db.Save();
        }

    }
}
