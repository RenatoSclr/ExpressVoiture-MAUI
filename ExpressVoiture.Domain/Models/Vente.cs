using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressVoiture.Domain.Models
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
