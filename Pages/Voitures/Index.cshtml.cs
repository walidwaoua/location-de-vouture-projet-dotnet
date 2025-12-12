using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LocationVoiture.Pages.Voitures
{
    public class VoituresIndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public VoituresIndexModel(ApplicationDbContext db) => _db = db;

        public IList<Voiture> Voitures { get; set; } = new List<Voiture>();

        public async Task OnGetAsync()
        {
            Voitures = await _db.Voitures.AsNoTracking().OrderBy(v => v.Marque).ToListAsync();
        }
    }
}
