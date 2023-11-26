using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCFirstApp.DataAcces.Repository.IRepository;
using MVCFirstApp.Models;
using MVCFirstApp.Models.ViewModels;
using MVCFirstApp.Utility;
using System.Data;

namespace MVCFirstApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
    public IActionResult Index()
        {
        List<Company> objCategoryList = _unitOfWork.Company.GetAll().ToList();

        return View(objCategoryList);
        }

    public IActionResult Upsert(int? id)
    {
        if(id == null || id == 0)
        {
            //create
            return View(new Company());
        }
        else
        {
            //update
            Company companyObj = _unitOfWork.Company.Get(Company => Company.Id == id);
            return View(companyObj);
        }

    }

    [HttpPost]
    public IActionResult Upsert(Company companyObj)
        {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if(file!= null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\Company");

                if (!string.IsNullOrEmpty(productVM.Company.ImageUrl))
                {
                    //delete old img
                    var oldImgPath = Path.Combine(wwwRootPath, productVM.Company.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImgPath))
                    {
                        System.IO.File.Delete(oldImgPath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                productVM.Company.ImageUrl = @"\images\Company\" + fileName;
            }
            
            if (productVM.Company.Id == 0)
            {
                _unitOfWork.Company.Add(productVM.Company);
            }
            else
            {
                _unitOfWork.Company.Update(productVM.Company);
            }
            _unitOfWork.Save();
            TempData["success"] = "Company successfully created";
            return RedirectToAction("Index");
        }
        else
        {
            productVM.CategoryList = _unitOfWork.Category.GetAll().Select(c =>

                new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                });
            };

            return View(productVM);
        }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Company> objCategoryList = _unitOfWork.Company.GetAll(includedProperties: "Category").ToList();
        return Json(new { data = objCategoryList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var productToDelete = _unitOfWork.Company.Get(p =>  p.Id == id);

        if (productToDelete == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        var oldImgPath = Path.Combine(_webHostEnvironment.WebRootPath, productToDelete.ImageUrl.TrimStart('\\'));

        if (System.IO.File.Exists(oldImgPath))
        {
            System.IO.File.Delete(oldImgPath);
        }

        _unitOfWork.Company.Remove(productToDelete);
        _unitOfWork.Save();

        return Json(new { success = true, message = "Delete successful" });
    }
    #endregion
}



