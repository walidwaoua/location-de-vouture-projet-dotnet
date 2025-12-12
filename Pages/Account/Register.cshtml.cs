using System.ComponentModel.DataAnnotations;
using LocationVoiture.Data;
using LocationVoiture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace LocationVoiture.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public RegisterModel(ApplicationDbContext db) => _db = db;

        [BindProperty, Required, MaxLength(50)]
        public string NomUtilisateur { get; set; } = string.Empty;

        [BindProperty, Required, MaxLength(100)]
        public string MotDePasse { get; set; } = string.Empty;

        [BindProperty]
        public Client Client { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Simple hash (pour démo). À remplacer par un vrai hashing salé (e.g. ASP.NET Identity, BCrypt, Argon2).
            string Hash(string input)
            {
                using var sha = SHA256.Create();
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToHexString(bytes);
            }

            _db.Clients.Add(Client);
            await _db.SaveChangesAsync();

            var compte = new Compte
            {
                NomUtilisateur = NomUtilisateur,
                MotDePasseHash = Hash(MotDePasse),
                ClientId = Client.Id
            };
            _db.Comptes.Add(compte);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
