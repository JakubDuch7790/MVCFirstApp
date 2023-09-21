﻿using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.DataAcces.Data;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;

namespace MVCFirstApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name != null && obj.Name.ToLower() == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Property name cannot be same as DisplayOrder");
            }
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category successfully created";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id== null || id==0)
            {
                return NotFound();
            }

            Category? wantedCategoryfromDB = _categoryRepo.Get(ID => ID.Id==id);
            //Category? wantedCategoryfromDB1 = _db.Categories.FirstOrDefault(c => c.Id == id);
            //Category? wantedCategoryfromDB2 = _db.Categories.Where(c=> c.Id == id).FirstOrDefault();

            if (wantedCategoryfromDB == null)
            {
                return NotFound();
            }

            return View(wantedCategoryfromDB);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category successfully updated";

                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? wantedCategoryfromDB = _categoryRepo.Get(ID => ID.Id == id);

            if (wantedCategoryfromDB == null)
            {
                return NotFound();
            }

            return View(wantedCategoryfromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _categoryRepo.Get(ID => ID.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category successfully removed";

            return RedirectToAction("Index");
        }
    }

}

