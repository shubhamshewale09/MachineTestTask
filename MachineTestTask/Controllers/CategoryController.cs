using MachineTestTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MachineTestTask.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        private  ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var category=db.Categories.ToList();

            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if(ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
 
        public ActionResult Edit(Category newcategory)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = db.Categories.FirstOrDefault(x=>x.CategoryId==newcategory.CategoryId);
                existingCategory.CategoryName = newcategory.CategoryName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newcategory);
        }

        public ActionResult Delete(int id)
        {
            if(ModelState.IsValid)
            { 
            var category= db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
       


    }
}