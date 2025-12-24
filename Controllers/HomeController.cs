using Microsoft.AspNetCore.Mvc;

namespace LocationVoiture.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToPage("/Index");
        }
    }
}
