using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ClientCreateModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public ClientCreateModel(ApplicationDbContext db) => _db = db;

    [BindProperty]
    public Client Client { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        _db.Clients.Add(Client);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
