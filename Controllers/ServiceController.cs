﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class ServiceController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            var services = context.Services.ToList();
            return View(services);
        }

        public ActionResult DeleteService(int id)
        {
            var service = context.Services.Find(id);
            context.Services.Remove(service);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateService(int id)
        {
            var service = context.Services.Find(id);
            return View(service);
        }

        [HttpPost]
        public ActionResult UpdateService(Service Services)
        {
            var service = context.Services.Find(Services.ServiceId);
            service.Title = Services.Title;
            service.Description = Services.Description;
            service.Icon = Services.Icon;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddService(Service newService)
        {
            context.Services.Add(newService);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}