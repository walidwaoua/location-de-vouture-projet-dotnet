using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class ClientsIndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public ClientsIndexModel(ApplicationDbContext db) => _db = db;

    public IList<Client> Clients { get; set; } = new List<Client>();

    public async Task OnGetAsync()
    {
        Clients = await _db.Clients.AsNoTracking().ToListAsync();
    }
}
