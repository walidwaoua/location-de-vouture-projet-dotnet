using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ClientDetailsModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public ClientDetailsModel(ApplicationDbContext db) => _db = db;

    public Client Client { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var entity = await _db.Clients.FindAsync(id);
        if (entity == null) return NotFound();
        Client = entity;
        return Page();
    }
}
