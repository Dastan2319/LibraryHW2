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
                return new MessageDTO { authorId=book.authorId,message=book.message };
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
                message=orderDto.message

            };
            db.Message.Create(message);
            db.Save();
        }

        public void SaveUpdate(MessageDTO orderDto)
        {
            Message message = new Message
            {
                authorId= orderDto.authorId,
                message= orderDto.message
            };
            db.Message.Update(message);
            db.Save();
        }

    }
}
