using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LocationVoiture.Pages.Admin.Users
{
    public class AdminUsersIndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public AdminUsersIndexModel(ApplicationDbContext db) => _db = db;

        public IList<Client> Items { get; set; } = new List<Client>();

        public async Task OnGetAsync()
        {
            Items = await _db.Clients.Include(c => c.Compte).AsNoTracking().ToListAsync();
        }
    }
}
