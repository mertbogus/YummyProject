using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class CategoryController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            var categories = context.Categories.ToList();
            return View(categories);
        }

        public ActionResult DeleteCategory(int id)
        {
            var value = context.Categories.Find(id);
            context.Categories.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            var value = context.Categories.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateCategoryt(Category Category)
        {
            var value = context.Categories.Find(Category.CategoryId);
            value.CategoryName = Category.CategoryName;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category Category)
        {
            context.Categories.Add(Category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}