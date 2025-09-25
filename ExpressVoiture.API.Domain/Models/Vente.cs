using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressVoiture.API.Domain.Models
{
    public class Vente
    {
        [Key]
        public int VenteId { get; set; }

        public DateTime DateDisponibiliteVente { get; set; }

        public double PrixVente { get; set; }

        public DateTime? DateVente { get; set; }

        public int VoitureId { get; set; }

        [ForeignKey("VoitureId")]
        public VoitureAVendre? Voiture { get; set; }

    }
}
