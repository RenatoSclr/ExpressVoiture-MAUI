using System.ComponentModel.DataAnnotations;

namespace ExpressVoiture.Shared.ViewModel
{
    public class DeleteVehicleViewModel
    {

        public int VoitureId { get; set; }
        public int Annee { get; set; }

        public string? Marque { get; set; }

        public string? Modele { get; set; }

        public string? Finition { get; set; }

    }
}
