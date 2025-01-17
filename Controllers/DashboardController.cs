﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;

namespace YummyProject.Controllers
{
    
    public class DashboardController : Controller
    {
        YummyContext context=new YummyContext();
        public ActionResult Index()
        {
            ViewBag.soupCount = context.Products.Count(x => x.Category.CategoryName == "Çorbalar");
            ViewBag.mostExpensive = context.Products.OrderByDescending(x => x.Price).Select(x => x.ProductName).FirstOrDefault();
            ViewBag.avgPrice=context.Products.Average(x => x.Price);
            ViewBag.cheapesPrice = context.Products.OrderBy(x => x.Price).Select(x => x.ProductName).FirstOrDefault();
            ViewBag.SweetCount = context.Products.Count(x => x.Category.CategoryName == "Tatlılar");
            ViewBag.CallHoutCount = context.Products.Count(x => x.Category.CategoryName == "Ara Sıcaklar");
            ViewBag.CommentCount = context.Testimonials.Count();
            ViewBag.MaxPriceProduct = context.Products.OrderByDescending(x => x.Price).Select(x => x.ProductName).FirstOrDefault();
            ViewBag.MinPriceProduct = context.Products.OrderBy(x => x.Price).Select(x => x.ProductName).FirstOrDefault();
            var value = context.Products.OrderByDescending(x => x.ProductId).ToList();

            return View(value);
        }
    }
}