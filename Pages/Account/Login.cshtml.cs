using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LocationVoiture.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public LoginModel(ApplicationDbContext db) => _db = db;

        [BindProperty, Required]
        public string NomUtilisateur { get; set; } = string.Empty;

        [BindProperty, Required, DataType(DataType.Password)]
        public string MotDePasse { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }

        private static string Hash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }

        public void OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var hash = Hash(MotDePasse);

            var admin = await _db.AdminUsers.FirstOrDefaultAsync(a => a.Email == NomUtilisateur && a.MotDePasseHash == hash);
            if (admin != null)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, admin.Email),
                    new(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                    new(ClaimTypes.Role, "Admin")
                };
                await SignInAsync(claims);
                return LocalRedirect(ReturnUrl ?? "/Admin/Index");
            }

            var compte = await _db.Comptes.Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.NomUtilisateur == NomUtilisateur && c.MotDePasseHash == hash);

            if (compte != null)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, compte.NomUtilisateur),
                    new(ClaimTypes.NameIdentifier, compte.Id.ToString()),
                    new(ClaimTypes.Role, "User"),
                };
                if (compte.ClientId != 0)
                {
                    claims.Add(new("ClientId", compte.ClientId.ToString()));
                }
                await SignInAsync(claims);
                return LocalRedirect(ReturnUrl ?? "/Index");
            }

            ModelState.AddModelError(string.Empty, "Nom d'utilisateur ou mot de passe invalide.");
            return Page();
        }

        private async Task SignInAsync(IEnumerable<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }
    }
}
