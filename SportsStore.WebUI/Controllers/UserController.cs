﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class UserController : Controller
    {
        private SSEntities db = new SSEntities();
        // GET: User
        public ActionResult Register()
        {
            ViewBag.Check = false;
            return View();
        }

        [HttpPost]
        public ActionResult Register( User user )
        {
            var datebase = db.Users;
            Order or = new Order();
            ViewBag.Check = false;
            bool check = true;
            foreach (var item in datebase)
            {
                if (user.Name == item.Name)
                {
                    ViewBag.Check = true;
                    ViewBag.Thank = "Such user is already registered";
                    check = false;
                }
            }
            if(check)
            {
                if (ModelState.IsValid)
                {
                    User user_new = new User
                    {
                        Name = user.Name,
                        Password = user.Password,
                        Address = user.Address,
                        City = user.City,
                        Country = user.Country,
                        State = user.State
                    };
                    db.Users.Add(user_new);
                    db.SaveChanges();
                    ViewBag.Check = true;
                    ViewBag.Thank = "Thank you";
                }
            }

            return View();
        }

        public ActionResult Log_in()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Log_in(LoginViewModel model)
        {
            var users = db.Users;
            foreach (var el in users)
            {
                if(el.Name == model.UserName && el.Password == model.Password)
                {
                    Session["user"] = el.UserID;
                    return RedirectToActionPermanent("List", "Product");
                }
            }
            if (ModelState.IsValid)
            {
                ViewBag.Mess = "Incorrect username or password";
            }
            else
            {
                ViewBag.Mess = null;
            }
            return View();
        }
        public RedirectToRouteResult rederict()
        {
            return RedirectToActionPermanent("List", "Product");
        }
    }
}