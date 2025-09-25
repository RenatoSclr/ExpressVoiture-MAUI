using ExpressVoiture.API.Domain.Models;
using ExpressVoiture.Services;
using ExpressVoiture.Services.IService;
using ExpressVoiture.Shared.ViewModel;
using Moq;
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

        //[Fact]
        //public void GetAllClientVehicleListViewModel_ReturnCorrectViewModelList()
        //{
        //    // Arrange
        //    var mockVoitureRepository = new Mock<IVoitureRepository>();
        //    var mockVoitureService = new Mock<IVehicleService>();
        //    mockVoitureRepository.Setup(repo => repo.GetAll(It.IsAny<string>()))
        //        .Returns(GetListVoitureAVendre());

        //    var homeService = new HomeService(mockVoitureRepository.Object, mockVoitureService.Object);
        //    // Act
        //    var result = homeService.GetAllClientVehicleListViewModel();
        //    // Assert
        //    Assert.True(result is List<ClientVehicleListViewModel>);
        //    Assert.Equal(3, result.Count);
        //    Assert.Equal("Mazda", result[0].Marque);
        //    Assert.Equal("Jeep", result[1].Marque);
        //    Assert.Equal("Renault", result[2].Marque);

        //}


        //[Fact]
        //public void GetClientDetailsViewModel_ReturnCorrectViewModel()
        //{
        //    // Arrange
        //    var voiture = GetVoitureAVendre();
        //    var mockVoitureRepository = new Mock<IVoitureRepository>();
        //    var mockVoitureService = new Mock<IVehicleService>();
        //    mockVoitureService.Setup(serv => serv.GetVoitureAVendreById(1))
        //                     .Returns(voiture);

        //    var homeService = new HomeService(mockVoitureRepository.Object, mockVoitureService.Object);

        //    // Act
        //    var result = homeService.GetClientDetailsViewModel(1);

        //    // Assert
        //    Assert.True(result is ClientDetailedVehicleViewModel);
        //    Assert.Equal(voiture.Annee, result.Annee);
        //    Assert.Equal(voiture.Modele, result.Modele);
        //    Assert.Equal(voiture.Marque, result.Marque);
        //    Assert.Equal(voiture.CodeVIN, result.CodeVIN);
        //    Assert.Equal(voiture.Finition, result.Finition);
        //    Assert.Equal(voiture.Reparation.Cout, result.CoutReparations);
        //    Assert.Equal(voiture.Reparation.Description, result.DescriptionReparations);
        //    Assert.Equal(voiture.Vente.DateVente, result.DateVente);
        //    Assert.Equal(voiture.Vente.DateDisponibiliteVente, result.DateDisponibiliteVente);
        //}
    }
}
