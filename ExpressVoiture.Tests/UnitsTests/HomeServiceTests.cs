using ExpressVoiture.API.Application.Services;
using ExpressVoiture.API.Domain.IRepository;
using ExpressVoiture.API.Domain.Models;
using ExpressVoiture.Shared.ViewModel;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace ExpressVoiture.Tests.UnitsTests
{
    public class HomeServiceTests
    {
        private List<VoitureAVendre> GetListVoitureAVendre()
        {
            var voitures = new List<VoitureAVendre>
            {
                new VoitureAVendre
                {
                    VoitureId = 1,
                    Annee = 2019,
                    Marque = "Mazda",
                    Modele = "Miata",
                    Finition = "LE",
                    DateAchat = new DateTime(2022, 1, 7),
                    PrixAchat = 1800,
                    Reparation = new Reparation {ReparationId = 1,  Description = "Restauration complète",Cout = 7600, VoitureId = 1},
                    Vente = new Vente{VenteId = 1, DateDisponibiliteVente = new DateTime(2022, 4, 7),PrixVente = 7600, DateVente = new DateTime(2022, 4, 8), VoitureId = 1}
                },
                new VoitureAVendre
                {
                    VoitureId = 2,
                    Annee = 2007,
                    Marque = "Jeep",
                    Modele = "Liberty",
                    Finition = "Sport",
                    DateAchat = new DateTime(2022, 4, 2),
                    PrixAchat = 4500,
                    Reparation = new Reparation { ReparationId = 2, Description = "Roulements des roues avant",Cout = 350, VoitureId = 2},
                    Vente = new Vente {  VenteId = 2,  DateDisponibiliteVente = new DateTime(2022, 4, 7),PrixVente = 5350, DateVente = new DateTime(2022, 4, 9),VoitureId = 2}
                },
                new VoitureAVendre
                {
                    VoitureId = 3,
                    Annee = 2007,
                    Marque = "Renault",
                    Modele = "Scénic",
                    Finition = "TCe",
                    DateAchat = new DateTime(2022, 4, 4),
                    PrixAchat = 180,
                    Reparation = new Reparation { ReparationId = 3, Description = "Radiateur, freins",  Cout = 690,VoitureId = 3},
                    Vente = new Vente{VenteId = 3,DateDisponibiliteVente = new DateTime(2022, 4, 8), PrixVente = 2990, VoitureId = 3 }
                }
            };
            return voitures;
        }

        private VoitureAVendre GetVoitureAVendre()
        {
            var voiture = new VoitureAVendre
            {
                VoitureId = 1,
                Annee = 2019,
                Marque = "Mazda",
                Modele = "Miata",
                Finition = "LE",
                DateAchat = new DateTime(2022, 1, 7),
                PrixAchat = 1800,
                Reparation = new Reparation { ReparationId = 1, Description = "Restauration complète", Cout = 7600, VoitureId = 1 },
                Vente = new Vente { VenteId = 1, DateDisponibiliteVente = new DateTime(2022, 4, 7), PrixVente = 7600, DateVente = new DateTime(2022, 4, 8), VoitureId = 1 }
            };
            return voiture;
        }

        [Fact]
        public async Task GetAllClientVehicleListViewModel_ReturnCorrectViewModelList()
        {
            // Arrange
            var mockVoitureRepository = new Mock<IVoitureRepository>();

            mockVoitureRepository.Setup(repo => repo.GetAll(It.IsAny<string>()))
                .ReturnsAsync(GetListVoitureAVendre());

            var voitureServiceApi = new VoitureService(mockVoitureRepository.Object);
            // Act
            var result = await voitureServiceApi.GetAllClientVehicle(includeProperties: "Reparation,Vente");
            var listResult = result.ToList();
            // Assert
            Assert.True(result is List<ClientVehicleListDto>);
            Assert.Equal(3, listResult.Count);
            Assert.Equal("Mazda", listResult[0].Marque);
            Assert.Equal("Jeep", listResult[1].Marque);
            Assert.Equal("Renault", listResult[2].Marque);

        }


        [Fact]
        public async Task GetClientDetailsViewModel_ReturnCorrectViewModel()
        {
            // Arrange
            var voiture = GetVoitureAVendre();
            var mockVoitureRepository = new Mock<IVoitureRepository>();

            mockVoitureRepository
                .Setup(r => r.Get(It.IsAny<Expression<Func<VoitureAVendre, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(voiture);

            var voitureService = new VoitureService(mockVoitureRepository.Object);

            // Act
            var result = await voitureService.GetClientDetailedVehicle(1);

            // Assert
            Assert.True(result is ClientDetailedVehicleDto);
            Assert.Equal(voiture.Annee, result.Annee);
            Assert.Equal(voiture.Modele, result.Modele);
            Assert.Equal(voiture.Marque, result.Marque);
            Assert.Equal(voiture.CodeVIN, result.CodeVIN);
            Assert.Equal(voiture.Finition, result.Finition);
            Assert.Equal(voiture.Reparation.Cout, result.CoutReparations);
            Assert.Equal(voiture.Reparation.Description, result.DescriptionReparations);
            Assert.Equal(voiture.Vente.DateVente, result.DateVente);
            Assert.Equal(voiture.Vente.DateDisponibiliteVente, result.DateDisponibiliteVente);
        }
    }
}
