﻿using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        IUsersService usersService;

        public AccountController(IUsersService serv)
        {
            usersService = serv;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                UsersDTO usersDtos = new UsersDTO();
                usersDtos.FIO = login.Email;
                usersDtos.password = login.Password;
                UsersDTO user= usersService.GetUsers(usersDtos);
                if (user.FIO == "true")
                {
                    return RedirectToAction("Register", "Account");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel registr)
        {
            if (ModelState.IsValid)
            {
                UsersDTO user = new UsersDTO();
                user.FIO = registr.Email;
                user.password = registr.Password;
                usersService.MakeUsers(user);
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

    }
}
