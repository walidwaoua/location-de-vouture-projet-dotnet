using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocationVoiture.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToPage("/Admin/Index");
        }
    }
}
