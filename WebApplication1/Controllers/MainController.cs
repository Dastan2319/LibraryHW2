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

        public MainController(IBookService serv)
        {
            bookService = serv;
        }
        public ActionResult Index()
        {
            IEnumerable<BookDTO> bookDtos = bookService.GetBook();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, BookViewModel>()).CreateMapper();
            var book = mapper.Map<IEnumerable<BookDTO>, List<BookViewModel>>(bookDtos);
            return View(book);
        }

        // GET: Main/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult EditOrCreate(int? id)
        {
            BookViewModel book = new BookViewModel();
            if (id != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, BookViewModel>()).CreateMapper();
                book = mapper.Map<BookDTO, BookViewModel>(bookService.GetBook(id));
            }
            return View(book);

        }

        [HttpPost]
        public ActionResult EditOrCreate(BookViewModel Books)
        {
            if (Books.Id != 0)
            {
                BookDTO book = new BookDTO();
                book.Images = Books.Images;
                book.Pages = Books.Pages;
                book.Price = Books.Price;
                book.Title = Books.Title;
                book.AuthorId = Books.AuthorId;
                bookService.SaveUpdate(book);
            }
            else
            {
                var bookDto = new BookDTO { Images = Books.Images, AuthorId = Books.AuthorId, Pages = Books.Pages, Price = Books.Price, Title = Books.Title };
                bookService.MakeBook(bookDto);
            }
            return RedirectToActionPermanent("Index", "Books");
        }

        // GET: Main/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Main/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
