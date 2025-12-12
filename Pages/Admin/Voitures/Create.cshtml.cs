using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LocationVoiture.Pages.Admin.Voitures
{
    public class AdminVoitureCreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public AdminVoitureCreateModel(ApplicationDbContext db) => _db = db;

        [BindProperty]
        public Voiture Voiture { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            _db.Voitures.Add(Voiture);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
