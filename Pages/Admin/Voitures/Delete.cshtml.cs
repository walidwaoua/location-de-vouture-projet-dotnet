using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LocationVoiture.Pages.Admin.Voitures
{
    public class AdminVoitureDeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public AdminVoitureDeleteModel(ApplicationDbContext db) => _db = db;

        [BindProperty]
        public Voiture Voiture { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var entity = await _db.Voitures.FindAsync(id);
            if (entity == null) return NotFound();
            Voiture = entity;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var entity = await _db.Voitures.FindAsync(Voiture.Id);
            if (entity == null) return NotFound();
            _db.Voitures.Remove(entity);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
