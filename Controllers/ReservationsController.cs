using Microsoft.AspNetCore.Mvc;

namespace LocationVoiture.Controllers
{
    public class ReservationsController : Controller
    {
        [HttpGet]
        public IActionResult Create(int voitureId)
        {
            return RedirectToPage("/Reservations/Create", new { voitureId });
        }
    }
}
