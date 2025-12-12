using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ClientDeleteModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public ClientDeleteModel(ApplicationDbContext db) => _db = db;

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
        var entity = await _db.Clients.FindAsync(Client.Id);
        if (entity == null) return NotFound();
        _db.Clients.Remove(entity);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
