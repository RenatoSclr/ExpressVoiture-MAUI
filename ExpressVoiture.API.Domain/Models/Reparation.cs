using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressVoiture.API.Domain.Models
{
    public class Reparation
    {
        [Key]
        public int ReparationId { get; set; }

        public string? Description { get; set; }

        public double Cout { get; set; }

        public int VoitureId { get; set; }

        [ForeignKey("VoitureId")]
        public VoitureAVendre? Voiture { get; set; }
    }
}
