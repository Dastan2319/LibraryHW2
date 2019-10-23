using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Service;
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
    public class UsersController : Controller
    {
        IBookService bookService;
        IUsersService usersService;
        IGanreService GanreService;
        public UsersController(IUsersService serv, IBookService bookServ,  IGanreService ganServ)
        {
            GanreService = ganServ;
            usersService = serv;
            bookService = bookServ;
        }
        public ActionResult Index()
        {
            var authorID= Request.Cookies["id"];
            if (authorID != null)
            {
                IEnumerable<BookDTO> bookDtos = bookService.GetBook().Where(x=>x.AuthorId==int.Parse( authorID.Value));
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, BookViewModel>()).CreateMapper();
                var book = mapper.Map<IEnumerable<BookDTO>, List<BookViewModel>>(bookDtos);
                return View(book);
            }
            else
            {
                return RedirectToActionPermanent("Login", "Account");
            }
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
                var bookDto = new BookDTO { Images = Books.Images, AuthorId = authorID, Pages = Books.Pages, Price = Books.Price, Title = Books.Title, Ganre = Books.Ganre };
                bookService.MakeBook(bookDto);
            }
            return RedirectToActionPermanent("Index", "Main");
        }

        public ActionResult EditOrCreateUser()
        {
            var authorID = int.Parse(Request.Cookies["id"].Value);
            UsersViewModel user = new UsersViewModel();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UsersDTO, UsersViewModel>()).CreateMapper();
            user = mapper.Map<UsersDTO, UsersViewModel>(usersService.GetUsers(authorID));            
            return View(user);
        }

        [HttpPost]
        public ActionResult EditOrCreateUser(UsersViewModel Users)
        {
            var authorID =int.Parse( Request.Cookies["id"].Value);
            var tempUser = usersService.GetUsers(authorID);
              tempUser.password = Users.password;

              usersService.SaveUpdate(tempUser);
            

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
