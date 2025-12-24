using Microsoft.AspNetCore.Mvc;

namespace LocationVoiture.Controllers
{
    public class VoituresController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToPage("/Voitures/Index");
        }
    }
}
