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

namespace WebApplication1.Controllers
{
    public class AuthorController : Controller
    {
        IAuthorService authorService;

        public AuthorController(IAuthorService serv)
        {
            authorService = serv;
        }

        public ActionResult Index()
        {

            Logger.Log.Info("Вход в Авторы");

            IEnumerable<AuthorsDTO> authorDtos = authorService.GetAuthor();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AuthorsDTO, AuthorViewModel>()).CreateMapper();
            var author = mapper.Map<IEnumerable<AuthorsDTO>, List<AuthorViewModel>>(authorDtos);
            return View(author);
        }
      
        public ActionResult EditOrCreate(int? id)
        {
            AuthorViewModel author=new AuthorViewModel();
         
            if (id != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AuthorsDTO, AuthorViewModel>()).CreateMapper();
                author = mapper.Map<AuthorsDTO, AuthorViewModel>(authorService.GetAuthor(id));
            }
            return View(author);

        }

        [HttpPost]
        public ActionResult EditOrCreate(Authors author)
        {
            
                if (author.Id != 0)
                {
                    var tempAuthor = authorService.GetAuthor(author.Id);
                    tempAuthor.FirstName = author.FirstName;
                    tempAuthor.LastName = author.LastName;
                    authorService.SaveUpdate(author);
                    
                }
                else
                {
                    var authorDto = new AuthorsDTO { LastName = author.LastName, FirstName = author.FirstName };
                    authorService.MakeAuthor(authorDto);
                }
            
            return RedirectToActionPermanent("Index", "Author");
        }

        public ActionResult Delete(int id)
        {        
            return RedirectToAction("Index", "Author");
        }
    }
}