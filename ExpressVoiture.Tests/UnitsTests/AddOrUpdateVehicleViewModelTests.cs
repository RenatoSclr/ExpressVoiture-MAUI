using ExpressVoiture.Shared.ViewModel;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ExpressVoiture.Tests.UnitsTests
{
    public class AddOrUpdateVehicleViewModelTests
    {
        private AddOrUpdateVehicleViewModel GetAddOrUpdateVehicleViewModel()
        {
            var voiture = new AddOrUpdateVehicleViewModel
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
                DescriptionReparations = "Restauration complète",
                ImagePath = ""

            };
            return voiture;
        }

        [Fact]
        public void AnneeIsNotValidWhenIsNotSuperiorOrEqualTo1990()
        {
            // Arrange
            var voiture = GetAddOrUpdateVehicleViewModel();
            voiture.Annee = 1989;

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(voiture, new ValidationContext(voiture), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Annee"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "L'année ne peut pas être inferieur à 1990 et supérieur a l'année actuelle.");
        }

        [Fact]
        public void AnneeIsNotValidWhenIsSuperiorToActualYear()
        {
            // Arrange
            var voiture = GetAddOrUpdateVehicleViewModel();
            voiture.Annee = DateTime.Now.Year + 1;

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(voiture, new ValidationContext(voiture), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Annee"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "L'année ne peut pas être inferieur à 1990 et supérieur a l'année actuelle.");
        }

        [Fact]
        public void AnneeIsValidWhenIsBetween1990AndActualYear()
        {
            // Arrange
            var voiture = GetAddOrUpdateVehicleViewModel();
            voiture.Annee = 1990;

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(voiture, new ValidationContext(voiture), validationResults, true);

            // Assert
            Assert.True(isValid);
            Assert.DoesNotContain(validationResults, vr => vr.MemberNames.Contains("Annee"));
           
        }

        [Fact]
        public void MarqueIsNotValidWhenEmpty()
        {
            // Arrange
            var voiture = GetAddOrUpdateVehicleViewModel();
            voiture.Marque = "";

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(voiture, new ValidationContext(voiture), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Marque"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "La marque est requise.");
        }

        [Fact]
        public void ModeleIsNotValidWhenEmpty()
        {
            // Arrange
            var voiture = GetAddOrUpdateVehicleViewModel();
            voiture.Modele = "";

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(voiture, new ValidationContext(voiture), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Modele"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Le modèle est requis.");
        }

        [Fact]
        public void FinitioIsNotValidWhenEmpty()
        {
            // Arrange
            var voiture = GetAddOrUpdateVehicleViewModel();
            voiture.Finition = "";

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(voiture, new ValidationContext(voiture), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Finition"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "La finition est requise.");
        }

        [Fact]
        public void DateAchatIsNotValidWhenIsGreaterOfTomorow()
        {
            // Arrange
            var voiture = GetAddOrUpdateVehicleViewModel();
            voiture.DateAchat = DateTime.Today.AddDays(1);

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(voiture, new ValidationContext(voiture), validationResults, true);

            // Assert
            Assert.False(isValid);   
        }

        [Fact]
        public void DateAchatIsNotValidWhenIsLowerOfAnnee()
        {
            // Arrange
            var voiture = GetAddOrUpdateVehicleViewModel();
            voiture.DateAchat = new DateTime(2022, 1, 7);
            voiture.Annee = 2023;

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(voiture, new ValidationContext(voiture), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("DateAchat"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "La date d'achat ne peut pas être inférieure à l'année de fabrication.");
           
        }


        [Fact]
        public void DateAchatIsValidWhenIsSuperiorOfAnnee()
        {
            // Arrange
            var voiture = GetAddOrUpdateVehicleViewModel();
            voiture.DateAchat = new DateTime(2022, 1, 7);
            voiture.Annee = 2021;

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(voiture, new ValidationContext(voiture), validationResults, true);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void PrixAchatIsNotValidWhenEqualToZero()
        {
            // Arrange
            var voiture = GetAddOrUpdateVehicleViewModel();
            voiture.PrixAchat = 0;

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(voiture, new ValidationContext(voiture), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("PrixAchat"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Le prix d'achat doit être supérieur à zéro.");
        }

        [Fact]
        public void CoutReparationsIsNotValidWhenIsLowerToZero()
        {
            // Arrange
            var voiture = GetAddOrUpdateVehicleViewModel();
            voiture.CoutReparations = -20;

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(voiture, new ValidationContext(voiture), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("CoutReparations"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Le coût des réparations doit être supérieur ou égal à zéro.");
        }

        [Fact]
        public void DateDisponibiliteVenteIsNotValidWhenIsLowerOfDateAchat()
        {
            // Arrange
            var voiture = GetAddOrUpdateVehicleViewModel();
            voiture.DateDisponibiliteVente = new DateTime(2021,1,7);
            voiture.DateAchat = new DateTime(2022, 1, 7);

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(voiture, new ValidationContext(voiture), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("DateDisponibiliteVente"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "La date de disponibilité à la vente ne peut pas être inférieure à la date d'achat.");
        }
        [Fact]
        public void DateVenteIsNotValidWhenIsLowerOfDateDisponibiliteVente()
        {
            // Arrange
            var voiture = GetAddOrUpdateVehicleViewModel();
            voiture.DateDisponibiliteVente = new DateTime(2022, 4, 7);
            voiture.DateVente = new DateTime(2021, 1, 7);

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(voiture, new ValidationContext(voiture), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("DateVente"));
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "La date de vente ne peut pas être inférieure à la date de disponibilité à la vente.");
        }
    }
}
