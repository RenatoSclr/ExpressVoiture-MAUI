using ExpressVoiture.API.Domain.Models;
using Xunit;

namespace ExpressVoiture.Tests.IntegrationsTests
{
    public class IntegrationsTestsOnExpressVoitureTestDatabase : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;

        public IntegrationsTestsOnExpressVoitureTestDatabase(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        private VoitureAVendre GetVoitureAVendre()
        {
            var voiture = new VoitureAVendre();

            voiture.Annee = 2021;
            voiture.Marque = "Toyota";
            voiture.Modele = "Corolla";
            voiture.Finition = "XLE";
            voiture.DateAchat = new DateTime(2023, 3, 15);
            voiture.PrixAchat = 20000;
            voiture.Reparation = new Reparation
            {
                Description = "Changement de moteur",
                Cout = 5000,
            };
            voiture.Vente = new Vente
            {
                DateDisponibiliteVente = new DateTime(2023, 6, 1),
                DateVente = new DateTime(2023, 6, 15),
            };
            return voiture;
        }

        [Fact]
        public void AddVehicleInTestDatabase()
        {
            // Arrange 
            VoitureAVendre vehicle = GetVoitureAVendre();
            

            // Act
            _fixture.AddVehicle(vehicle);

            // Assert 
            VoitureAVendre vehicleAdd = _fixture.GetVehicle(vehicle.VoitureId);
            Assert.NotNull(vehicleAdd);
            Assert.Equal(vehicle.Annee, vehicleAdd.Annee);
            Assert.Equal(vehicle.Modele, vehicleAdd.Modele);
            Assert.Equal(vehicle.Reparation.Description, vehicleAdd.Reparation.Description);
            Assert.Equal(vehicle.Vente.DateVente, vehicleAdd.Vente.DateVente);
        }

        [Fact]
        public void DeleteVehicleFromTestDatabase()
        {
            // Arrange 
            VoitureAVendre vehicle = GetVoitureAVendre();

            // Act
            _fixture.AddVehicle(vehicle);
            VoitureAVendre AddedVehicle = _fixture.GetVehicle(vehicle.VoitureId);
            Assert.NotNull(AddedVehicle);

            _fixture.DeleteVehicle(vehicle);
            // Assert 
            VoitureAVendre deletedVehicle = _fixture.GetVehicle(vehicle.VoitureId);
            Assert.Null(deletedVehicle);
        }

        [Fact]
        public void UpdateVehicleFromDatabase()
        {
            // Arrange 
            VoitureAVendre vehicle = GetVoitureAVendre();
            _fixture.AddVehicle(vehicle);

            // Mise à jour des propriétés du véhicule
            vehicle.Annee = 2020;
            vehicle.Reparation.Cout = 8000;
            vehicle.Vente.DateDisponibiliteVente = new DateTime(2022, 4, 10);

            // Act
            _fixture.UpdateVehicle(vehicle);

            // Assert 
            VoitureAVendre updatedVehicle = _fixture.GetVehicle(vehicle.VoitureId);

            Assert.NotNull(updatedVehicle);
            Assert.Equal(2020, updatedVehicle.Annee);
            Assert.Equal(8000, updatedVehicle.Reparation.Cout);
            Assert.Equal(new DateTime(2022, 4, 10), updatedVehicle.Vente.DateDisponibiliteVente);

            Assert.Equal("Toyota", updatedVehicle.Marque);
            Assert.Equal("Corolla", updatedVehicle.Modele);
            Assert.Equal("XLE", updatedVehicle.Finition);
            Assert.Equal("Changement de moteur", updatedVehicle.Reparation.Description);
            Assert.Equal(new DateTime(2023, 6, 15), updatedVehicle.Vente.DateVente);
        }
    }
}
