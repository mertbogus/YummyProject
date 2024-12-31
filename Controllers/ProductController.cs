﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class ProductController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }

        public ActionResult DeleteProduct(int id)
        {
            var value = context.Products.Find(id);
            context.Products.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            List<SelectListItem> category = (from x in context.Categories
                                                select new SelectListItem
                                                {
                                                    Text = x.CategoryName,
                                                    Value = x.CategoryId.ToString()
                                                }).ToList();
            ViewBag.kategoriler = category;

            var value = context.Products.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateProduct(Product Product)
        {
            var old = context.Products.Find(Product.ProductId);
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var saveLocation = currentDirectory + "Images\\Product\\";
            var fileName = Path.Combine(saveLocation + Product.ImageFile.FileName);
            Product.ImageFile.SaveAs(fileName);
            old.ImageUrl = "/Images/Product/" + Product.ImageFile.FileName;
            old.Ingredients = Product.Ingredients;
            old.Price = Product.Price;
            old.CategoryId = Product.CategoryId;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            List<SelectListItem> kategoriler = (from x in context.Categories
                                                select new SelectListItem
                                                {
                                                    Text = x.CategoryName,
                                                    Value = x.CategoryId.ToString()
                                                }).ToList();
            kategoriler.Insert(0, new SelectListItem
            {
                Text = "Lütfen kategori seçiniz.",
                Value = "",
                Selected = true,
                Disabled = true
            });
            ViewBag.kategoriler = kategoriler;
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product newProduct)
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var saveLocation = currentDirectory + "Images\\Product\\";
            var fileName = Path.Combine(saveLocation + newProduct.ImageFile.FileName);
            newProduct.ImageFile.SaveAs(fileName);
            newProduct.ImageUrl = "/Images/Products/" + newProduct.ImageFile.FileName;
            context.Products.Add(newProduct);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}