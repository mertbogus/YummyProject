using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class ContactInfoefault1Controller : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            var infos = context.ContactInfos.ToList();
            return View(infos);
        }

        public ActionResult DeleteInfo(int id)
        {
            var value = context.ContactInfos.Find(id);
            context.ContactInfos.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddInfo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddInfo(ContactInfo newInfo)
        {
            context.ContactInfos.Add(newInfo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateInfo(int id)
        {
            var value = context.ContactInfos.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateInfo(ContactInfo newInfo)
        {
            var Info = context.ContactInfos.Find(newInfo.ContactInfoId);
            Info.PhoneNumber = newInfo.PhoneNumber;
            Info.MapUrl = newInfo.MapUrl;
            Info.OpenHours = newInfo.OpenHours;
            Info.Address = newInfo.Address;
            Info.Email = newInfo.Email;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}