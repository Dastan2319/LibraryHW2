using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class BooksController : Controller
    {
        IBookService bookService;
        IMessageService messageService;
        public BooksController(IBookService serv,IMessageService mesServ)
        {
            messageService = mesServ;
            bookService = serv;
        }

        public ActionResult Index()
        {
            IEnumerable<BookDTO> bookDtos = bookService.GetBook();            
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, BookViewModel>()).CreateMapper();
            var book = mapper.Map<IEnumerable<BookDTO>, List<BookViewModel>>(bookDtos);
            
            return View(book);
        }

        public ActionResult EditOrCreate(int? id)
        {
            var authorID = Request.Cookies["id"];
            BookViewModel book = new BookViewModel();

            if (id != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, BookViewModel>()).CreateMapper();
                book = mapper.Map<BookDTO, BookViewModel>(bookService.GetBook(id));
                if (book.AuthorId == int.Parse(authorID.Value))
                {
                    return View(book);
                }
                else
                {
                    return RedirectToActionPermanent("Index", "Main");
                }



            }
            return View(book);

        }

        [HttpPost]
        public ActionResult EditOrCreate(BookViewModel Books)
        {
            var authorID = Int32.Parse(Request.Cookies["id"].Value);
            if (Books.Id != 0)
            {
                var tempBook = bookService.GetBook(Books.Id);
                tempBook.Id = Books.Id;
                tempBook.Images = Books.Images;
                tempBook.Pages = Books.Pages;
                tempBook.Price = Books.Price;
                tempBook.Title = Books.Title;
                tempBook.AuthorId = authorID;
                bookService.SaveUpdate(tempBook);
            }
            else
            {
                var bookDto = new BookDTO { Images = Books.Images, AuthorId = authorID, Pages = Books.Pages, Price = Books.Price, Title = Books.Title };
                bookService.MakeBook(bookDto);
            }
            return RedirectToActionPermanent("Index", "Main");
        }

        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index", "Books");
        }
    }
}