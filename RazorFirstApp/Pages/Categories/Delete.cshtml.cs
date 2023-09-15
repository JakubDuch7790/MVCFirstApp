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
            Category = _db.Categories.FirstOrDefault(c => c.Id == id);
        }

        public IActionResult OnPost()
        {
            _db.Remove(Category);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
