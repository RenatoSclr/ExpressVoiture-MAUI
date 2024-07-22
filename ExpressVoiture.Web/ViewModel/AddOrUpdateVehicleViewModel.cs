
using System.ComponentModel.DataAnnotations;

namespace ExpressVoiture.ViewModel
{
    public class AddOrUpdateVehicleViewModel : IValidatableObject
    {
        public int VoitureId { get; set; }

        [Required(ErrorMessage = "L'année est requise.")]
        [Display(Name = "Année")]
        public int Annee { get; set; }

        [Required(ErrorMessage = "La marque est requise.")]
        public string? Marque { get; set; }

        [Required(ErrorMessage = "Le modèle est requis.")]
        [Display(Name = "Modèle")]
        public string? Modele { get; set; }

        [Required(ErrorMessage = "La finition est requise.")]
        public string? Finition { get; set; }

        [Required(ErrorMessage = "La date d'achat est requise.")]
        [Display(Name = "Date d'achat")]
        [DataType(DataType.Date)]
        public DateTime DateAchat { get; set; }

        [Required(ErrorMessage = "Le prix d'achat est requis.")]
        [Display(Name = "Prix d'achat")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix d'achat doit être supérieur à zéro.")]
        public double PrixAchat { get; set; }


        public string? CodeVIN { get; set; }

        [Display(Name = "Description des réparations")]
        public string? DescriptionReparations { get; set; }

        [Required(ErrorMessage = "Le coût des réparations est requis.")]
        [Display(Name = "Coût des réparations")]
        [Range(0, double.MaxValue, ErrorMessage = "Le coût des réparations doit être supérieur ou égal à zéro.")]
        public double CoutReparations { get; set; }

        [Required(ErrorMessage = "La date de disponibilité à la vente est requise.")]
        [Display(Name = "Date de disponibilité à la vente")]
        [DataType(DataType.Date)]
        public DateTime DateDisponibiliteVente { get; set; }

        [Display(Name = "Date de vente (Si vendu)")]
        [DataType(DataType.Date)]
        public DateTime? DateVente { get; set; }

        [Display(Name = "Selectionner une image")]
        public string? ImagePath { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (Annee < 1990 || Annee > DateTime.Now.Year)
            {
                validationResults.Add(new ValidationResult("L'année ne peut pas être inferieur à 1990 et supérieur a l'année actuelle.", new[] { nameof(Annee) }));
            }

            if (DateAchat.Year < Annee)
            {
                validationResults.Add(new ValidationResult("La date d'achat ne peut pas être inférieure à l'année de fabrication.", new[] { nameof(DateAchat) }));
            }
            if (DateAchat.Year > DateTime.Now.Year)
            {
                validationResults.Add(new ValidationResult("La date d'achat ne peut pas être supérieur à l'année actuelle.", new[] { nameof(DateAchat) }));
            }

            if (DateDisponibiliteVente < DateAchat)
            {
                validationResults.Add(new ValidationResult("La date de disponibilité à la vente ne peut pas être inférieure à la date d'achat.", new[] { nameof(DateDisponibiliteVente) }));
            }

            if (DateVente.HasValue && DateVente.Value < DateDisponibiliteVente)
            {
                validationResults.Add(new ValidationResult("La date de vente ne peut pas être inférieure à la date de disponibilité à la vente.", new[] { nameof(DateVente) }));
            }

            return validationResults;
        }
    }
}
