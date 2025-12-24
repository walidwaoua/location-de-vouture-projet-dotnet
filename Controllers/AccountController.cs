using Microsoft.AspNetCore.Mvc;

namespace LocationVoiture.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return RedirectToPage("/Account/Login", new { returnUrl });
        }

        [HttpGet]
        public IActionResult Register()
        {
            return RedirectToPage("/Account/Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            return RedirectToPage("/Account/Logout");
        }
    }
}
