using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class ClientEditModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public ClientEditModel(ApplicationDbContext db) => _db = db;

    [BindProperty]
    public Client Client { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var entity = await _db.Clients.FindAsync(id);
        if (entity == null) return NotFound();
        Client = entity;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        _db.Attach(Client).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
