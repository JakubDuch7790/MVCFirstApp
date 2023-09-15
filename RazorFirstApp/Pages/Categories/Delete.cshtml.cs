using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorFirstApp.Data;
using RazorFirstApp.Models;

namespace RazorFirstApp.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int? id)
        {
            if(id != null && id != 0)
            {
                Category = _db.Categories.FirstOrDefault(c => c.Id == id);
            }
        }

        public IActionResult OnPost()
        {
            Category? obj = _db.Categories.Find(Category.Id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Remove(obj);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
