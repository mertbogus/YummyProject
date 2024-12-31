using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class ChefSocialsController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            var socials = context.ChefSocials.ToList();
            return View(socials);
        }

        public ActionResult DeleteChefSocial(int id)
        {
            var value = context.ChefSocials.Find(id);
            context.ChefSocials.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateChefSocials(int id)
        {
            var value = context.ChefSocials.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateChefSocials(ChefSocial social)
        {
            var value = context.ChefSocials.Find(social.ChefSocialId);
            value.SocialUrl = social.SocialUrl;

            if (social.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "Images\\SocialChef\\";
                var fileName = Path.Combine(saveLocation + social.ImageFile.FileName);
                social.ImageFile.SaveAs(fileName);
                social.Icon = "Images/SocialChef/" + social.ImageFile.FileName;
                value.Icon = social.Icon;
            }
           value.SocialMediaName = social.SocialMediaName;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddChefSocials()
        {
            List<SelectListItem> şefler = (from i in context.Chefs
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.ChefId.ToString()
                                           }).ToList();
            şefler.Insert(0, new SelectListItem
            {
                Text = "Bir şef seçiniz.",
                Value = "",
                Selected = true,
                Disabled = true
            });
            ViewBag.şefler = şefler;
            return View();
        }
        [HttpPost]
        public ActionResult AddChefSocials(ChefSocial newChefSocial)
        {
            if (newChefSocial.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "Images\\SocialChef\\";
                var fileName = Path.Combine(saveLocation + newChefSocial.ImageFile.FileName);
                newChefSocial.ImageFile.SaveAs(fileName);
                newChefSocial.Icon = "Images/SocialChef/" + newChefSocial.ImageFile.FileName;
            }
            context.ChefSocials.Add(newChefSocial);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}