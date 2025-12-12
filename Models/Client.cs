using System;
using System.ComponentModel.DataAnnotations;

namespace LocationVoiture.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nom { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Prenom { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Adresse { get; set; } = string.Empty;

        [Range(18, 120)]
        public int Age { get; set; }

        [Required, MaxLength(50)]
        public string NumeroPermis { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string CIN { get; set; } = string.Empty;

        public Compte? Compte { get; set; }
    }
}