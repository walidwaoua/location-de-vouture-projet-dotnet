using LocationVoiture.Models;
using Microsoft.EntityFrameworkCore;

namespace LocationVoiture.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Compte> Comptes => Set<Compte>();
        public DbSet<Voiture> Voitures => Set<Voiture>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<AdminUser> AdminUsers => Set<AdminUser>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.Compte)
                .WithOne(a => a.Client)
                .HasForeignKey<Compte>(a => a.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Voiture>()
                .HasIndex(v => v.Matricule)
                .IsUnique();

            // Precision for decimal column to avoid truncation warnings
            modelBuilder.Entity<Voiture>()
                .Property(v => v.PrixParJour)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Reservation>()
                .HasIndex(r => r.NumeroReservation)
                .IsUnique();
        }
    }
}