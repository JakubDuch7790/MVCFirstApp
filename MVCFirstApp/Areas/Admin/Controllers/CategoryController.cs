using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCFirstApp.DataAcces.Data;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;
using MVCFirstApp.Utility;

namespace MVCFirstApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
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
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category successfully created";
            return RedirectToAction("Index");
        }
        return View();
    }
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? wantedCategoryfromDB = _unitOfWork.Category.Get(ID => ID.Id == id);
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
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
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

        Category? wantedCategoryfromDB = _unitOfWork.Category.Get(ID => ID.Id == id);

        if (wantedCategoryfromDB == null)
        {
            return NotFound();
        }

        return View(wantedCategoryfromDB);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? obj = _unitOfWork.Category.Get(ID => ID.Id == id);

        if (obj == null)
        {
            return NotFound();
        }

        _unitOfWork.Category.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category successfully removed";

        return RedirectToAction("Index");
    }
}

