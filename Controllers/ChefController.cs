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
    public class ChefController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            var chefslist = context.Chefs.ToList();
            return View(chefslist);
        }

        public ActionResult DeleteChef(int id)
        {
            var value = context.Chefs.Find(id);
            context.Chefs.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateChef(int id)
        {
            var value = context.Chefs.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateChef(Chef chef)
        {
            var usedchefs = context.Chefs.Find(chef.ChefId);
            if (chef.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "Images\\Chefs\\";
                var fileName = Path.Combine(saveLocation + chef.ImageFile.FileName);
                chef.ImageFile.SaveAs(fileName);
                chef.ImageUrl = "/Images/Chefs/" + chef.ImageFile.FileName;
            }
            else
            {
                chef.ImageUrl = usedchefs.ImageUrl;
            }
           
            usedchefs.Name = chef.Name;
            usedchefs.Title = chef.Title;
            usedchefs.Description = chef.Description;
            usedchefs.ChefSocials = chef.ChefSocials;
            usedchefs.ImageUrl = chef.ImageUrl;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddChefs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddChefs(Chef Chef)
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var saveLocation = currentDirectory + "Images\\Chefs\\";
            var fileName = Path.Combine(saveLocation + Chef.ImageFile.FileName);
            Chef.ImageFile.SaveAs(fileName);
            Chef.ImageUrl = "/Images/Chefs/" + Chef.ImageFile.FileName;
            context.Chefs.Add(Chef);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}