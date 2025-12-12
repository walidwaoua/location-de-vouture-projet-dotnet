using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LocationVoiture.Pages.Reservations
{
    public class ReservationCreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ReservationCreateModel(ApplicationDbContext db) => _db = db;

        [BindProperty]
        public Reservation Reservation { get; set; } = new();

        public void OnGet(int voitureId)
        {
            Reservation.VoitureId = voitureId;
            Reservation.DateReservation = DateTime.UtcNow;
            Reservation.NumeroReservation = $"RES-{DateTimeOffset.UtcNow:yyyyMMddHHmmss}";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            _db.Reservations.Add(Reservation);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Voitures/Index");
        }
    }
}
