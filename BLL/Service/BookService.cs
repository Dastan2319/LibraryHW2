using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DLL;
using DLL.Entity;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class BookService : IBookService
    {
        IUnitOfWork db { get; set; }
        IMessageService msg { get; set; }
        public BookService(IUnitOfWork uow,IMessageService ims)
        {
            msg = ims;
            db = uow;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public BookDTO GetBook(int? id)
        {
            if (id != null)
            {
                var book = db.Books.Get(id);
                var message=msg.GetMessage().Where(x => x.bookId == book.Id);
                return new BookDTO {Id=book.Id,AuthorId=book.AuthorId, Title= book.Title,Pages= book.Pages,Price= book.Price,Images= book.Images,message= message };
            }
            else
            {
                return new BookDTO();
            }
        }

        public IEnumerable<BookDTO> GetBook()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Books, BookDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Books>, List<BookDTO>>(db.Books.GetAll());
        }

        public void MakeBook(BookDTO orderDto)
        {
            Users user = db.Users.Get(orderDto.AuthorId);
            Books book = new Books
            {
                Title = orderDto.Title,
                Price = orderDto.Price,
                Pages = orderDto.Pages,
                Images = orderDto.Images,
                AuthorId= user.Id
            };
            db.Books.Create(book);
            db.Save();
        }

        public void SaveUpdate(BookDTO orderDto)
        {
            Users user = db.Users.Get(orderDto.AuthorId);
            Books book = new Books
            {
                Id=orderDto.Id,
                AuthorId = user.Id,
                Title = orderDto.Title,
                Price = orderDto.Price,
                Pages = orderDto.Pages,
                Images = orderDto.Images,
            };
            db.Books.Update(book);
            db.Save();
        }

        public void Delete(int id)
        {
            db.Books.Delete(id);
            db.Save();
        }
    }
}
