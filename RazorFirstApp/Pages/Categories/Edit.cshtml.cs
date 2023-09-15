using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorFirstApp.Data;
using RazorFirstApp.Models;

namespace RazorFirstApp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        public Category Category { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int? id) 
        {
            if (id != 0 && id != null)
            {
                Category = _db.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Category);
                _db.SaveChanges();
                TempData["success"] = "Category successfully edited";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
