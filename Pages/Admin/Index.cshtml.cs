using LocationVoiture.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LocationVoiture.Pages.Admin
{
    public class AdminIndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public AdminIndexModel(ApplicationDbContext db) => _db = db;

        public int VoituresCount { get; private set; }
        public int ReservationsCount { get; private set; }
        public int ClientsCount { get; private set; }
        public int ComptesCount { get; private set; }

        public async Task OnGetAsync()
        {
            VoituresCount = await _db.Voitures.CountAsync();
            ReservationsCount = await _db.Reservations.CountAsync();
            ClientsCount = await _db.Clients.CountAsync();
            ComptesCount = await _db.Comptes.CountAsync();
        }
    }
}
