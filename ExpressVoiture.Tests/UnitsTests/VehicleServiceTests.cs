using Moq;
using Xunit;
using ExpressVoiture.Services;
using System.Linq.Expressions;
using ExpressVoiture.Services.IService;
using Microsoft.AspNetCore.Http;
using ExpressVoiture.API.Domain.Models;
using ExpressVoiture.API.Domain.IRepository;
using ExpressVoiture.Shared.ViewModel;
using ExpressVoiture.API.Application.Services;

namespace ExpressVoiture.Tests.UnitsTests
{
    public class VehicleServiceTests
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
                Reparation = new Reparation {ReparationId = 1,  Description = "Restauration complète",Cout = 7600, VoitureId = 1},
                Vente = new Vente{VenteId = 1, DateDisponibiliteVente = new DateTime(2022, 4, 7),PrixVente = 7600, DateVente = new DateTime(2022, 4, 8), VoitureId = 1}    
            };
            return voiture;
        }

        [Fact]
        public async Task GetAllVoitureAVendreViewModel_ReturnCorrectViewModelList()
        {
            // Arrange
            var mockVoitureRepository = new Mock<IVoitureRepository>();
            mockVoitureRepository.Setup(repo => repo.GetAll(It.IsAny<string>()))
                .ReturnsAsync(GetListVoitureAVendre());

            var voitureService = new VoitureService(mockVoitureRepository.Object);
            // Act
            var result = await voitureService.GetListAdminVehicleViewModel();

            var listResult = result.ToList();
            // Assert
            Assert.True(result is List<AdminVehicleListDto>);
            Assert.Equal(3, result.Count);
            Assert.Equal("Mazda", result[0].Marque);
            Assert.Equal("Jeep", result[1].Marque);
            Assert.Equal("Renault", result[2].Marque);

        }

        [Fact]
        public async Task GetAddOrUpdateVehicleViewModel_ReturnCorrectViewModel()
        {
            // Arrange
            var voiture = GetVoitureAVendre();
            var mockVoitureRepository = new Mock<IVoitureRepository>();
            mockVoitureRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<VoitureAVendre, bool>>>(), "Reparation,Vente"))
                             .ReturnsAsync(voiture);

            var voitureService = new VoitureService(mockVoitureRepository.Object);

            // Act
            var result = await voitureService.GetAddOrUpdateVehicleViewModel(1);

            // Assert
            Assert.True(result is AddOrUpdateVehicleDto);
            Assert.Equal(voiture.VoitureId, result.VoitureId);
            Assert.Equal(voiture.Annee, result.Annee);
            Assert.Equal(voiture.Modele, result.Modele);
            Assert.Equal(voiture.Marque, result.Marque);
            Assert.Equal(voiture.CodeVIN, result.CodeVIN);
            Assert.Equal(voiture.Finition, result.Finition);
            Assert.Equal(voiture.PrixAchat, result.PrixAchat);
            Assert.Equal(voiture.DateAchat, result.DateAchat);
            Assert.Equal(voiture.Reparation.Cout, result.CoutReparations);
            Assert.Equal(voiture.Reparation.Description, result.DescriptionReparations);
            Assert.Equal(voiture.Vente.DateVente, result.DateVente);
            Assert.Equal(voiture.Vente.DateDisponibiliteVente, result.DateDisponibiliteVente);


        }

        [Fact]
        public async Task SaveVoitureAVendre_ShouldAddAndSaveVoiture()
        {
            // Arrange
            var voitureAAjouter = new AddOrUpdateVehicleDto
            {
                Annee = 2019,
                Marque = "Mazda",
                Modele = "Miata",
                Finition = "LE",
                DateAchat = new DateTime(2022, 1, 7),
                PrixAchat = 1800,
                CodeVIN = "123456789",
                CoutReparations = 7600,
                DateDisponibiliteVente = new DateTime(2022, 4, 7),
                DateVente = new DateTime(2022, 4, 8),
                DescriptionReparations = "Restauration complète"
            };

            var mockVoitureRepository = new Mock<IVoitureRepository>();


            var voitureService = new VoitureService(mockVoitureRepository.Object);

            // Act
            await voitureService.SaveVoitureAVendre(voitureAAjouter);

            // Assert
            mockVoitureRepository.Verify(repo => repo.Add(It.Is<VoitureAVendre>(v =>
                v.VoitureId == voitureAAjouter.VoitureId &&
                v.Annee == voitureAAjouter.Annee &&
                v.Marque == voitureAAjouter.Marque &&
                v.Modele == voitureAAjouter.Modele &&
                v.Finition == voitureAAjouter.Finition &&
                v.DateAchat == voitureAAjouter.DateAchat &&
                v.PrixAchat == voitureAAjouter.PrixAchat &&
                v.CodeVIN == voitureAAjouter.CodeVIN &&
                v.Reparation.Cout == voitureAAjouter.CoutReparations &&
                v.Reparation.Description == voitureAAjouter.DescriptionReparations &&
                v.Vente.DateDisponibiliteVente == voitureAAjouter.DateDisponibiliteVente &&
                v.Vente.DateVente == voitureAAjouter.DateVente
            )), Times.Once);

            mockVoitureRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public async Task UpdateVoitureAVendre_ShouldUpdateAndSaveVoiture()
        {
            // Arrange
            var voitureAAjouter = new AddOrUpdateVehicleDto
            {
                VoitureId = 1,
                Annee = 2019,
                Marque = "Mazda",
                Modele = "Miata",
                Finition = "LE",
                DateAchat = new DateTime(2022, 1, 7),
                PrixAchat = 1800,
                CodeVIN = "123456789",
                CoutReparations = 7600,
                DateDisponibiliteVente = new DateTime(2022, 4, 7),
                DateVente = new DateTime(2022, 4, 8),
                DescriptionReparations = "Restauration complète"
            };

            VoitureAVendre voitureAVendre = GetVoitureAVendre();

            var mockVoitureRepository = new Mock<IVoitureRepository>();

            mockVoitureRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<VoitureAVendre, bool>>>(), "Reparation,Vente"))
                .ReturnsAsync(voitureAVendre);

            var voitureService = new VoitureService(mockVoitureRepository.Object);

            // Act
            await voitureService.UpdateVoitureAVendre(voitureAAjouter);

            // Assert
            mockVoitureRepository.Verify(repo => repo.Update(It.Is<VoitureAVendre>(v =>
                v.VoitureId == voitureAAjouter.VoitureId &&
                v.Annee == voitureAAjouter.Annee &&
                v.Marque == voitureAAjouter.Marque &&
                v.Modele == voitureAAjouter.Modele &&
                v.Finition == voitureAAjouter.Finition &&
                v.DateAchat == voitureAAjouter.DateAchat &&
                v.PrixAchat == voitureAAjouter.PrixAchat &&
                v.CodeVIN == voitureAAjouter.CodeVIN &&
                v.Reparation.Cout == voitureAAjouter.CoutReparations &&
                v.Reparation.Description == voitureAAjouter.DescriptionReparations &&
                v.Vente.DateDisponibiliteVente == voitureAAjouter.DateDisponibiliteVente &&
                v.Vente.DateVente == voitureAAjouter.DateVente
            )), Times.Once);

            mockVoitureRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public async Task DeleteVoitureAVendre_ShouldDeleteVehicule()
        {
            // Arrange
            var voitureASupprimer = GetVoitureAVendre();
            var mockVoitureRepository = new Mock<IVoitureRepository>();
            mockVoitureRepository
                .Setup(repo => repo.Get(It.IsAny<Expression<Func<VoitureAVendre, bool>>>(), "Reparation,Vente"))
                .ReturnsAsync(voitureASupprimer);

            var voitureService =  new VoitureService(mockVoitureRepository.Object);
            // Act
            await voitureService.DeleteVoitureAVendre(voitureASupprimer.VoitureId);

            // Assert
            mockVoitureRepository.Verify(repo => repo.Remove(It.IsAny<VoitureAVendre>()), Times.Once);

            mockVoitureRepository.Verify(repo => repo.Save(), Times.Once);
        }


        [Fact]
        public async Task GetDeleteVehicleViewModel_ReturnCorrectViewModel()
        {
            // Arrange
            var voiture = GetVoitureAVendre();
            var mockVoitureRepository = new Mock<IVoitureRepository>();
            mockVoitureRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<VoitureAVendre, bool>>>(), null))
            .ReturnsAsync(voiture);

            var voitureService = new VoitureService(mockVoitureRepository.Object);

            // Act
            var result = await voitureService.GetDeleteVehicleViewModel(voiture.VoitureId);

            // Assert
            Assert.True(result is DeleteVehicleDto);
            Assert.Equal(voiture.VoitureId, result.VoitureId);
            Assert.Equal(voiture.Annee, result.Annee);
            Assert.Equal(voiture.Modele, result.Modele);
            Assert.Equal(voiture.Marque, result.Marque);
            Assert.Equal(voiture.Finition, result.Finition);
        }

    }
}
