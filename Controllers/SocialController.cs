using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class SocialController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            var socials = context.SocialMedias.ToList();
            return View(socials);
        }

        public ActionResult DeleteSocial(int id)
        {
            var value = context.SocialMedias.Find(id);
            context.SocialMedias.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateSocial(int id)
        {
            var value = context.SocialMedias.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateSocial(SocialMedia Social)
        {
            var social = context.SocialMedias.Find(Social.SocialMediaId);
            social.Url = Social.Url;
            social.SocialMediaName = Social.SocialMediaName;
            social.Icon = Social.Icon;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddSocial()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSocial(SocialMedia Social)
        {
            context.SocialMedias.Add(Social);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}