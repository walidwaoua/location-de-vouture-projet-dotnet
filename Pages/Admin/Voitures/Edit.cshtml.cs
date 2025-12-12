using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class AdminVoitureEditModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public AdminVoitureEditModel(ApplicationDbContext db) => _db = db;

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
        if (!ModelState.IsValid) return Page();
        _db.Attach(Voiture).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
