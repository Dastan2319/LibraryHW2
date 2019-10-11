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

        public BooksController(IBookService serv)
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
        public ActionResult EditOrCreate(Books Books)
        {

            if (Books.Id != 0)
            {
                var tempBooks = bookService.GetBook(Books.Id);
                tempBooks.AuthorId = Books.AuthorId;
                tempBooks.Title = Books.Title;
                tempBooks.Images = Books.Images;
                tempBooks.Pages = Books.Pages;
                tempBooks.Price = Books.Price;

            }
            else
            {
                var bookDto = new BookDTO {Images= Books.Images,AuthorId= Books.AuthorId,Pages= Books.Pages,Price= Books.Price,Title= Books.Title};
                bookService.MakeBook(bookDto);
            }
            return RedirectToActionPermanent("Index", "Books");
        }

        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index", "Books");
        }
    }
}