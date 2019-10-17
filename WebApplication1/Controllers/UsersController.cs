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
        IUsersService usersService;

        public UsersController(IUsersService serv)
        {
            usersService = serv;
        }
        public ActionResult Index()
        {
            var authorID = int.Parse(Request.Cookies["id"].Value);
            UsersViewModel user = new UsersViewModel();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UsersDTO, UsersViewModel>()).CreateMapper();
            user = mapper.Map<UsersDTO, UsersViewModel>(usersService.GetUsers(authorID));            
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(UsersViewModel Users)
        {
            var authorID =int.Parse( Request.Cookies["id"].Value);
            var tempUser = usersService.GetUsers(authorID);
              tempUser.password = Users.password;

              usersService.SaveUpdate(tempUser);
            

            return RedirectToActionPermanent("Index", "Main");
        }
        
    }
}
