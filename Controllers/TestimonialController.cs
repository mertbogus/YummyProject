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
    public class TestimonialController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            var testimonials = context.Testimonials.ToList();
            return View(testimonials);
        }

        public ActionResult DeleteTestimonial(int id)
        {
            var testimonial = context.Testimonials.Find(id);
            context.Testimonials.Remove(testimonial);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateTestimonial(int id)
        {
            var testimonial = context.Testimonials.Find(id);
            return View(testimonial);
        }

        [HttpPost]
        public ActionResult UpdateTestimonial(Testimonial Testimonials)
        {
            var testimonial = context.Testimonials.Find(Testimonials.TestimonialId);
            if (Testimonials.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "Images\\Testimonial\\";
                var fileName = Path.Combine(saveLocation + Testimonials.ImageFile.FileName);
                Testimonials.ImageFile.SaveAs(fileName);
                testimonial.ImageUrl = "/Images/Testimonial/" + Testimonials.ImageFile.FileName;
            }
            testimonial.NameSurname = Testimonials.NameSurname;
            testimonial.Title = Testimonials.Title;
            testimonial.Comments = Testimonials.Comments;
            testimonial.Rating = Testimonials.Rating;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddTestimonial()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTestimonial(Testimonial Testimonials)
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var saveLocation = currentDirectory + "Images\\Testimonial\\";
            var fileName = Path.Combine(saveLocation + Testimonials.ImageFile.FileName);
            Testimonials.ImageFile.SaveAs(fileName);
            Testimonials.ImageUrl = "/Images/Testimonial/" + Testimonials.ImageFile.FileName;
            context.Testimonials.Add(Testimonials);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}