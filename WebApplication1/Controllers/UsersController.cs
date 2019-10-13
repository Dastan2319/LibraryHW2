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
            IEnumerable<UsersDTO> usersDtos = usersService.GetUsers();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UsersDTO, UsersViewModel>()).CreateMapper();
            var user = mapper.Map<IEnumerable<UsersDTO>, List<UsersViewModel>>(usersDtos);
            return View(user);
        }

        public ActionResult EditOrCreate(int? id)
        {
            UsersViewModel user = new UsersViewModel();

            if (id != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UsersDTO, UsersViewModel>()).CreateMapper();
                user = mapper.Map<UsersDTO, UsersViewModel>(usersService.GetUsers(id));
            }
            return View(user);

        }

        [HttpPost]
        public ActionResult EditOrCreate(Users Users)
        {

            if (Users.Id != 0)
            {
                var tempUser = usersService.GetUsers(Users.Id);
                tempUser.FIO = Users.FIO;
                tempUser.Id = Users.Id;
                tempUser.password = Users.password;

                usersService.SaveUpdate(tempUser);
            }
            else
            {
                var usersDto = new UsersDTO { FIO=Users.FIO };
                usersService.MakeUsers(usersDto);
            }

            return RedirectToActionPermanent("Index", "Users");
        }

        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index", "Users");
        }
    }
}
