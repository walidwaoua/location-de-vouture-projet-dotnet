using System;
using System.ComponentModel.DataAnnotations;

namespace LocationVoiture.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string NumeroReservation { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string NomReservateur { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string PrenomReservateur { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Adresse { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string NumeroPermis { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Telephone { get; set; } = string.Empty;

        [Required]
        public DateTime DateReservation { get; set; }

        public int? ClientId { get; set; }
        public Client? Client { get; set; }

        public int? VoitureId { get; set; }
        public Voiture? Voiture { get; set; }
    }
}