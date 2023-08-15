using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
       private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name","The DisplayOrder doesnot exactly match the name.");
            }
            if(ModelState.IsValid)
            {
                 _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Sucess"] = "Category Sucesfully Created";
                 return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Edith(int?  id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category  categoryFromDb = _db.Categories.Find(id);
            // Category?  categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id );
            // Category?  categoryFromDb2 = _db.Categories.Where( u => u.Id == id ).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edith(Category obj)
        {
            
            if(ModelState.IsValid)
            {
                 _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Sucess"] = "Category  Sucesfully Edith";
                 return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int?  id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category  categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost ,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
           Category?  obj = _db.Categories.Find(id);
            if(ModelState.IsValid)
            {
                 _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["Sucess"] = "Category  Sucesfully Delete";
                 return RedirectToAction("Index");
            }
            return View();
        }


        
        
    }
}