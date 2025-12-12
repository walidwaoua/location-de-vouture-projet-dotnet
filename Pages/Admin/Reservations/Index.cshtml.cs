using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class AdminReservationsIndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public AdminReservationsIndexModel(ApplicationDbContext db) => _db = db;

    public IList<Reservation> Items { get; set; } = new List<Reservation>();

    public async Task OnGetAsync()
    {
        Items = await _db.Reservations
            .Include(r => r.Voiture)
            .AsNoTracking()
            .OrderByDescending(r => r.DateReservation)
            .ToListAsync();
    }
}
