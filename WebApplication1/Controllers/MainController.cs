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

namespace WebApplication1.Controllers
{
    public class MainController : Controller
    {
        IBookService bookService;
        IMessageService messageService;
        public MainController(IBookService serv, IMessageService mesServ)
        {
            messageService = mesServ;
            bookService = serv;
        }
        public ActionResult Index()
        {
            var authorID = Request.Cookies["id"];
            if (authorID != null)
            {
                IEnumerable<BookDTO> bookDtos = bookService.GetBook();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, BookViewModel>()).CreateMapper();
                var book = mapper.Map<IEnumerable<BookDTO>, List<BookViewModel>>(bookDtos);
                return View(book);
            }
            else
            {
                return RedirectToActionPermanent("Login", "Account");
            }
        }

        // GET: Main/Details/5
        public ActionResult Details(int id)
        {
            var authorID = Request.Cookies["id"];
            if (authorID != null)
            {
                HttpContext.Response.Cookies["Bookid"].Value = id + "";
                BookViewModel book = new BookViewModel();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, BookViewModel>()).CreateMapper();
                book = mapper.Map<BookDTO, BookViewModel>(bookService.GetBook(id));
                return View(book);
            }
            else
            {
                return RedirectToActionPermanent("Login", "Account");
            }
        }
        
        [HttpPost]
        public ActionResult Comment(string msg) 
        {
            var bookID = Int32.Parse(Request.Cookies["Bookid"].Value);
            var authorID=Int32.Parse(Request.Cookies["id"].Value);
            var messageDto = new MessageDTO { bookId = bookID, authorId =authorID, message = msg };
            messageService.MakeMessage(messageDto);
            return RedirectToActionPermanent("Index", "Details/" + bookID);
        }
        [HttpGet]
        public ActionResult EditOrCreate(int? id)
        {
            var authorID = Request.Cookies["id"];
            if (authorID != null)
            {
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
            else
            {
                return RedirectToActionPermanent("Login", "Account");
            }
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
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            BookViewModel book = new BookViewModel();
            var authorID = Request.Cookies["id"];
            if (authorID != null)
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
            else
            {
                return RedirectToActionPermanent("Index", "Main");
            }
        }
        // GET: Main/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            
            bookService.Delete(id);
            return RedirectToActionPermanent("Index", "Main");


        }
        
    }
}
