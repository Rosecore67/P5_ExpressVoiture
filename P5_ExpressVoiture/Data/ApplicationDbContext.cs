using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P5_ExpressVoiture.Models.Entities;

namespace P5_ExpressVoiture.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<Utilisateur, UserRole, string>(options)
    {
        public DbSet<Voiture> Voitures { get; set; }
        public DbSet<Marque> Marques { get; set; }
        public DbSet<Modele> Modeles { get; set; }
        public DbSet<Reparation> Reparations { get; set; }
        public DbSet<TypeReparation> TypeReparations { get; set; }
        public DbSet<Finance> Finances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration de la relation entre Voiture et Finance
            modelBuilder.Entity<Voiture>()
                .HasOne(v => v.Finance)
                .WithOne(f => f.Voiture)
                .HasForeignKey<Finance>(f => f.VoitureID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration de la relation entre Voiture et Réparation
            modelBuilder.Entity<Reparation>()
                .HasOne(r => r.Voiture)
                .WithMany(v => v.Reparations)
                .HasForeignKey(r => r.VoitureID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration de la relation entre Réparation et TypeRéparation
            modelBuilder.Entity<Reparation>()
                .HasOne(r => r.TypeReparation)
                .WithMany()
                .HasForeignKey(r => r.TypeReparationID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation entre Voiture et Marque
            modelBuilder.Entity<Voiture>()
                .HasOne(v => v.Marque)
                .WithMany()
                .HasForeignKey(v => v.MarqueID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation entre Voiture et Modele
            modelBuilder.Entity<Voiture>()
                .HasOne(v => v.Modele)
                .WithMany()
                .HasForeignKey(v => v.ModeleID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration de la propriété NomRole pour être requis
            modelBuilder.Entity<UserRole>()
                .Property(r => r.NomRole)
                .IsRequired();

            // Configuration des propriétés decimal
            modelBuilder.Entity<Finance>()
                .Property(f => f.PrixAchat)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Finance>()
                .Property(f => f.PrixVente)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Reparation>()
                .Property(r => r.CoutReparation)
                .HasColumnType("decimal(18, 2)");

            // Autres configurations de relations et contraintes uniques
            modelBuilder.Entity<Voiture>()
                .HasIndex(v => v.CodeVIN)
                .IsUnique();
        }
    }
}
