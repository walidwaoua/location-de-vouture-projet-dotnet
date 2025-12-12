using System.ComponentModel.DataAnnotations;

namespace LocationVoiture.Models
{
    public class Voiture
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Matricule { get; set; } = string.Empty;

        [Range(0, 100000)]
        public decimal PrixParJour { get; set; }

        [Required, MaxLength(100)]
        public string Marque { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Modele { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [MaxLength(300)]
        public string? ImageUrl { get; set; }

        public bool Disponible { get; set; } = true;
    }
}