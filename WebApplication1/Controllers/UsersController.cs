﻿using AutoMapper;
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

        //public ActionResult Index()
        //{
        //    IEnumerable<UsersDTO> usersDtos = usersService.GetUsers();
        //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UsersDTO, UsersViewModel>()).CreateMapper();
        //    var user = mapper.Map<IEnumerable<UsersDTO>, List<UsersViewModel>>(usersDtos);
        //    return View(user/*.Where(x=>x.Id=="тут id пользователя")*/);
        //}

        public ActionResult Index()
        {
            //UsersViewModel user = new UsersViewModel();
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UsersDTO, UsersViewModel>()).CreateMapper();
            //user = mapper.Map<UsersDTO, UsersViewModel>(usersService.GetUsers("id пользователя"));            
            //return View(user);
            return View();
        }

        [HttpPost]
        public ActionResult Index(UsersViewModel Users)
        {
            var tempUser = usersService.GetUsers(Users.Id);
              tempUser.password = Users.password;

              usersService.SaveUpdate(tempUser);
            

            return RedirectToActionPermanent("Index", "EditOrCreate");
        }
        
    }
}
