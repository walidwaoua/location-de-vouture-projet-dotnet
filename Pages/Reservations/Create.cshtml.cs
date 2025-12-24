using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LocationVoiture.Pages.Reservations
{
    [Authorize(Roles = "User")]
    public class ReservationCreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ReservationCreateModel(ApplicationDbContext db) => _db = db;

        [BindProperty]
        public Reservation Reservation { get; set; } = new();

        public decimal PrixParJour { get; private set; }

        private int? GetClientId()
        {
            var claim = User.FindFirst("ClientId")?.Value;
            return int.TryParse(claim, out var id) ? id : null;
        }

        public IActionResult OnGet(int voitureId)
        {
            Reservation.VoitureId = voitureId;
            Reservation.DateReservation = DateTime.UtcNow;
            Reservation.NumeroReservation = $"RES-{DateTimeOffset.UtcNow:yyyyMMddHHmmss}";
            Reservation.DateDebut = DateTime.Today;
            Reservation.DateFin = DateTime.Today.AddDays(1);

            var voiture = _db.Voitures.AsNoTracking().FirstOrDefault(v => v.Id == voitureId);
            PrixParJour = voiture?.PrixParJour ?? 0;
            if (voiture != null)
            {
                var joursInit = Math.Max(1, (Reservation.DateFin.Date - Reservation.DateDebut.Date).Days);
                Reservation.PrixTotal = voiture.PrixParJour * joursInit;
            }

            var clientId = GetClientId();
            if (clientId == null)
            {
                return RedirectToPage("/Account/Login", new { returnUrl = Url.Page("/Reservations/Create", new { voitureId }) });
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var clientId = GetClientId();
            if (clientId == null)
            {
                return RedirectToPage("/Account/Login", new { returnUrl = Url.Page("/Reservations/Create", new { Reservation.VoitureId }) });
            }

            if (Reservation.VoitureId == null || Reservation.VoitureId == 0)
            {
                ModelState.AddModelError(string.Empty, "Aucune voiture sélectionnée.");
                TempData["ErrorMessage"] = "Aucune voiture sélectionnée.";
                return Page();
            }

            var voiture = await _db.Voitures.AsNoTracking().FirstOrDefaultAsync(v => v.Id == Reservation.VoitureId);
            if (voiture == null)
            {
                TempData["ErrorMessage"] = "Voiture introuvable.";
                return Page();
            }
            PrixParJour = voiture.PrixParJour;

            if (!voiture.Disponible)
            {
                ModelState.AddModelError(string.Empty, "La voiture est introuvable ou indisponible.");
                TempData["ErrorMessage"] = "La voiture est introuvable ou indisponible.";
                return Page();
            }

            if (Reservation.DateFin <= Reservation.DateDebut)
            {
                ModelState.AddModelError(string.Empty, "La date de fin doit être après la date de début.");
                TempData["ErrorMessage"] = "La date de fin doit être après la date de début.";
                return Page();
            }

            var overlap = await _db.Reservations.AnyAsync(r => r.VoitureId == Reservation.VoitureId
                && r.Id != Reservation.Id
                && r.DateDebut < Reservation.DateFin
                && Reservation.DateDebut < r.DateFin);
            if (overlap)
            {
                ModelState.AddModelError(string.Empty, "La voiture est déjà réservée sur cette période.");
                TempData["ErrorMessage"] = "La voiture est déjà réservée sur cette période.";
                return Page();
            }

            var jours = Math.Max(1, (Reservation.DateFin.Date - Reservation.DateDebut.Date).Days);
            Reservation.PrixTotal = voiture.PrixParJour * jours;

            Reservation.ClientId = clientId;
            if (Reservation.DateReservation == default)
                Reservation.DateReservation = DateTime.UtcNow;
            if (string.IsNullOrWhiteSpace(Reservation.NumeroReservation))
                Reservation.NumeroReservation = $"RES-{DateTimeOffset.UtcNow:yyyyMMddHHmmssfff}-{Random.Shared.Next(1000, 9999)}";

            ModelState.Clear();
            if (!TryValidateModel(Reservation))
            {
                TempData["ErrorMessage"] = "Veuillez corriger les champs requis.";
                return Page();
            }

            _db.Reservations.Add(Reservation);
            try
            {
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Réservation enregistrée avec succès.";
                return RedirectToPage("/Voitures/Index");
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "Impossible d'enregistrer la réservation pour le moment. Veuillez réessayer.";
                ModelState.AddModelError(string.Empty, "Impossible d'enregistrer la réservation pour le moment. Veuillez réessayer.");
                return Page();
            }
        }
    }
}
