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
        IGanreService GanreService;
        public MainController(IBookService serv, IMessageService mesServ,IGanreService ganServ)
        {
            messageService = mesServ;
            bookService = serv;
            GanreService = ganServ;
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
        [HttpPost]
        public ActionResult Comment(string msg,string rating) 
        {
            var bookID = Int32.Parse(Request.Cookies["Bookid"].Value);
            var authorID=Int32.Parse(Request.Cookies["id"].Value);
            var messageDto = new MessageDTO { bookId = bookID, authorId =authorID, message = msg ,rating=int.Parse( rating)};
            messageService.SaveUpdate(messageDto);
            return RedirectToActionPermanent("Index", "Details/" + bookID);
        }
        public ActionResult EditOrCreate(int? id)
        {
            
            var authorID = Request.Cookies["id"];
            List<string> items = new List<string>();
            foreach (var item in GanreService.GetGanre())
            {
                items.Add(item.FirstName);
            }

            ViewBag.Ganre = items;
            BookViewModel book = new BookViewModel();
                
                if (id != 0)
                {
               
                HttpContext.Response.Cookies["Bookid"].Value = id + "";
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
                tempBook.Ganre = Books.Ganre;
                bookService.SaveUpdate(tempBook);
            }
            else
            {
                var bookDto = new BookDTO { Images = Books.Images, AuthorId = authorID, Pages = Books.Pages, Price = Books.Price, Title = Books.Title,Ganre=Books.Ganre };
                bookService.MakeBook(bookDto);
            }
            return RedirectToActionPermanent("Index", "Main");
        }
        // GET: Main/Details/5
        public ActionResult Details(int id)
        {
            HttpContext.Response.Cookies["Bookid"].Value = id + "";
            var authorID = Request.Cookies["id"];
            if (authorID != null)
            {
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

    }
}
