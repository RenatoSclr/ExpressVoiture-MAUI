using ExpressVoiture.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoiture.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<VoitureAVendre> Voitures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VoitureAVendre>()
               .HasOne(v => v.Reparation)
               .WithOne(r => r.Voiture)
               .HasForeignKey<Reparation>(r => r.VoitureId);

            modelBuilder.Entity<VoitureAVendre>()
                .HasOne(v => v.Vente)
                .WithOne(s => s.Voiture)
                .HasForeignKey<Vente>(s => s.VoitureId);

            modelBuilder.Entity<VoitureAVendre>().HasData(
                    new VoitureAVendre { VoitureId = 1, Annee = 2019, Marque = "Mazda", Modele = "Miata", Finition = "LE", DateAchat = new DateTime(2022, 1, 7), PrixAchat = 1800, ImagePath = "images\\vehicles\\d950d976-c3db-4e9a-a465-6422ef06f949.jpg" },
                    new VoitureAVendre { VoitureId = 2, Annee = 2007, Marque = "Jeep", Modele = "Liberty", Finition = "Sport", DateAchat = new DateTime(2022, 4, 2), PrixAchat = 4500, ImagePath = "images\\vehicles\\ba52fc34-a4eb-446f-aa19-078a26829f29.jpg" },
                    new VoitureAVendre { VoitureId = 3, Annee = 2007, Marque = "Renault", Modele = "Scénic", Finition = "TCe", DateAchat = new DateTime(2022, 4, 4), PrixAchat = 1800, ImagePath = "images\\vehicles\\41597198-aaea-461b-af70-c9b4505e9298.jpg" },
                    new VoitureAVendre { VoitureId = 4, Annee = 2017, Marque = "Ford", Modele = "Explorer", Finition = "XLT", DateAchat = new DateTime(2022, 4, 5), PrixAchat = 24350, ImagePath = "images\\vehicles\\fa6b80a2-f4f9-44da-bf16-6e8bd5e2d1a0.jpg" },
                    new VoitureAVendre { VoitureId = 5, Annee = 2008, Marque = "Honda", Modele = "Civic", Finition = "LX", DateAchat = new DateTime(2022, 4, 6), PrixAchat = 4000, ImagePath = "images\\vehicles\\633fd8d9-4550-4269-a508-5a08e1fa564b.jpg" },
                    new VoitureAVendre { VoitureId = 6, Annee = 2016, Marque = "Volkswagen", Modele = "GTI", Finition = "S", DateAchat = new DateTime(2022, 4, 6), PrixAchat = 15250, ImagePath = "images\\vehicles\\f0c751ce-6cf0-4fcf-9ac8-1f125269dec3.jpg" },
                    new VoitureAVendre { VoitureId = 7, Annee = 2013, Marque = "Ford", Modele = "Edge", Finition = "SEL", DateAchat = new DateTime(2022, 4, 7), PrixAchat = 10990, ImagePath = "images\\vehicles\\c93a06dd-2e25-4776-91f4-d999779cad87.jpg" }
                );

            modelBuilder.Entity<Reparation>().HasData(
                    new Reparation { ReparationId = 1, Description = "Restauration complète", Cout = 7600, VoitureId = 1 },
                    new Reparation { ReparationId = 2, Description = "Roulements des roues avant", Cout = 350, VoitureId = 2 },
                    new Reparation { ReparationId = 3, Description = "Radiateur, freins", Cout = 690, VoitureId = 3 },
                    new Reparation { ReparationId = 4, Description = "Pneus, freins", Cout = 1100, VoitureId = 4 },
                    new Reparation { ReparationId = 5, Description = "Climatisation, freins", Cout = 475, VoitureId = 5 },
                    new Reparation { ReparationId = 6, Description = "Pneus", Cout = 440, VoitureId = 6 },
                    new Reparation { ReparationId = 7, Description = "Pneus, freins, climatisation", Cout = 950, VoitureId = 7 }
                );

            modelBuilder.Entity<Vente>().HasData(
                    new Vente { VenteId = 1, DateDisponibiliteVente = new DateTime(2022, 4, 7), PrixVente = 7600, DateVente = new DateTime(2022, 4, 8), VoitureId = 1 },
                    new Vente { VenteId = 2, DateDisponibiliteVente = new DateTime(2022, 4, 7), PrixVente = 5350, DateVente = new DateTime(2022, 4, 9), VoitureId = 2 },
                    new Vente { VenteId = 3, DateDisponibiliteVente = new DateTime(2022, 4, 8), PrixVente = 2990, VoitureId = 3 },
                    new Vente { VenteId = 4, DateDisponibiliteVente = new DateTime(2022, 4, 9), PrixVente = 25950, VoitureId = 4 },
                    new Vente { VenteId = 5, DateDisponibiliteVente = new DateTime(2022, 4, 9), PrixVente = 4975, VoitureId = 5 },
                    new Vente { VenteId = 6, DateDisponibiliteVente = new DateTime(2022, 4, 10), PrixVente = 16190, DateVente = new DateTime(2022, 4, 12), VoitureId = 6 },
                    new Vente { VenteId = 7, DateDisponibiliteVente = new DateTime(2022, 4, 11), PrixVente = 12440, DateVente = new DateTime(2022, 4, 12), VoitureId = 7 }
                );

        }
    }
}
