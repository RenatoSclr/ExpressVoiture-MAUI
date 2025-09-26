using System.ComponentModel.DataAnnotations;

namespace ExpressVoiture.Shared.ViewModel
{
    public class AdminVehicleListDto
    {
        public int VoitureId { get; set; }
        public int Annee { get; set; }


        public string? Marque { get; set; }


        public string? Modele { get; set; }


        public string? Finition { get; set; }

        public DateTime DateAchat { get; set; }

        public double PrixAchat { get; set; }

        public string? CodeVIN { get; set; }

        public string? DescriptionReparations { get; set; }
        public double CoutReparations { get; set; }
        public DateTime DateDisponibiliteVente { get; set; }

        public double PrixVente { get; set; }
       
        public DateTime? DateVente { get; set; }
    }

}
