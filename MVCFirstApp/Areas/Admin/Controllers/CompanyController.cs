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
//[Authorize(Roles = SD.Role_Admin)]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            if (companyObj.Id == 0)
            {
                _unitOfWork.Company.Add(companyObj);
                TempData["success"] = "Company successfully created";
            }
            else
            {
                _unitOfWork.Company.Update(companyObj);
                TempData["success"] = "Company successfully updated";
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        else
        {
            return View(companyObj);
        }
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Company> objCategoryList = _unitOfWork.Company.GetAll().ToList();
        return Json(new { data = objCategoryList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var companyToDelete = _unitOfWork.Company.Get(c =>  c.Id == id);

        if (companyToDelete == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        _unitOfWork.Company.Remove(companyToDelete);
        _unitOfWork.Save();

        return Json(new { success = true, message = "Delete successful" });
    }
    #endregion
}



