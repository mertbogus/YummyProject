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
    public class EventController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            var events = context.Events.ToList();
            return View(events);
        }

        [HttpGet]
        public ActionResult AddEvent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEvent(Event Events)
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var saveLocation = currentDirectory + "Images\\Events\\";
            var fileName = Path.Combine(saveLocation + Events.ImageFile.FileName);
            Events.ImageFile.SaveAs(fileName);
            Events.ImageUrl = "/Images/Events/" + Events.ImageFile.FileName;
            context.Events.Add(Events);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteEvent(int id)
        {
            var value = context.Events.Find(id);
            context.Events.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateEvent(int id)
        {
            var value = context.Events.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateEvent(Event Events)
        {
            var value = context.Events.Find(Events.EventId);
            if (Events.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "Images\\Events\\";
                var fileName = Path.Combine(saveLocation + Events.ImageFile.FileName);
                Events.ImageFile.SaveAs(fileName);
                value.ImageUrl = "/Images/Events/" + Events.ImageFile.FileName;
            }

            value.Title = Events.Title;
            value.Description = Events.Description;
            value.Price = Events.Price;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}