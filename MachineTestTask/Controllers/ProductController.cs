using MachineTestTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MachineTestTask.Controllers
{
    public class ProductController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int page = 1, int pageSize = 5) 
        {
            var totalProducts = db.Products.Count();
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            var products = db.Products.Include("Category")
                                      .OrderBy(p => p.ProductId)
                                      .Skip((page - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(products);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.Categories.ToList();
            return View(product);
        }
        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            ViewBag.Categories = db.Categories.ToList();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = db.Products.FirstOrDefault(x => x.ProductId == newProduct.ProductId);
               existingProduct.ProductName = newProduct.ProductName;
                    existingProduct.CategoryId = newProduct.CategoryId;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                
            }

            ViewBag.Categories = db.Categories.ToList();
            return View(newProduct);
        }
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var Product = db.Products.Find(id);
                db.Products.Remove(Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}