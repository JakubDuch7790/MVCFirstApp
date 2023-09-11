using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.Data;
using MVCFirstApp.Models;

namespace MVCFirstApp.Controllers
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
            if (obj.Name != null && obj.Name.ToLower() == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Property name cannot be same as DisplayOrder");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
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

            Category? wantedCategoryfromDB = _db.Categories.Find(id);
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
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
