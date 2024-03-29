﻿using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
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
            IEnumerable<Category> categoryList = _db.Categories;
            return View(categoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                //key propety name dari model category
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                TempData["success"] = "successfully add category";
                _db.SaveChanges();

                //RedirectToAction: setelah submit dikembalikan ke halaman list category
                //exp: RedirectToAction("Index") / RedirectToAction("Index", "Category")
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var categoryById = _db.Categories.Find(id);
            if (categoryById == null)
                return NotFound();

            return View(categoryById);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //key propety name dari model category
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                TempData["success"] = "successfully edit category";

                _db.SaveChanges();

                //RedirectToAction: setelah submit dikembalikan ke halaman list category
                //exp: RedirectToAction("Index") / RedirectToAction("Index", "Category")
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var categoryById = _db.Categories.Find(id);
            if (categoryById == null)
                return NotFound();

            return View(categoryById);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var categoryById = _db.Categories.Find(id);
            if (categoryById == null)
                return NotFound();
            
            _db.Categories.Remove(categoryById);
            TempData["success"] = "successfully delete category";

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
