using System.ComponentModel.DataAnnotations;

namespace LocationVoiture.Models
{
    public class Compte
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string NomUtilisateur { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string MotDePasseHash { get; set; } = string.Empty;

        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}