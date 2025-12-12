using System.ComponentModel.DataAnnotations;

namespace LocationVoiture.Models
{
    public class AdminUser
    {
        [Key]
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string MotDePasseHash { get; set; } = string.Empty;
    }
}