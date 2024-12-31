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
   
    public class FeautureController : Controller
    {
        YummyContext context=new YummyContext();
        private object newFeature;

        public ActionResult Index()
        {
            var values = context.Features.ToList();
            return View(values);
        }

        public ActionResult AddFeature()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFeature(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return View(feature);
            }

            context.Features.Add(feature);
            int result = context.SaveChanges();
            if (result == 0)
            {
                ViewBag.error = "Değerler kaydedilirken bir hata ile karşılaşıldı";
                return View(feature);
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteFeature(int id)
        {
            var features = context.Features.Find(id);
            context.Features.Remove(features);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateFeature(int id)
        {
            var feature = context.Features.Find(id);
            return View(feature);
        }

        [HttpPost]
        public ActionResult UpdateFeature(Feature Features)
        {
            var feature = context.Features.Find(Features.FeatureId);

            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var saveLocation = currentDirectory + "Images\\Features\\";
            var fileName = Path.Combine(saveLocation + Features.ImageFile.FileName);
            Features.ImageFile.SaveAs(fileName);
            feature.ImageUrl = "/Images/Features/" + Features.ImageFile.FileName;
            feature.Title = Features.Title;
            feature.Description = Features.Description;
            feature.VideoUrl = Features.VideoUrl;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}