using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class MessageController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            var messages = context.Messages.ToList();
            return View(messages);
        }

        public ActionResult DeleteMessage(int id)
        {
            var value = context.Messages.Find(id);
            context.Messages.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateMessage(int id)
        {
            var value = context.Messages.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateMessage(Message Messages)
        {
            var oldMessage = context.Messages.Find(Messages.MessageId);
            oldMessage.Name = Messages.Name;
            oldMessage.Email = Messages.Email;
            oldMessage.Subject = Messages.Subject;
            oldMessage.MessageContent = Messages.MessageContent;
            oldMessage.IsRead = Messages.IsRead;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult IsRead(int id)
        {
            var messages = context.Messages.Find(id);
            messages.IsRead = true;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddMessage(Message Messages)
        {
            context.Messages.Add(Messages);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}