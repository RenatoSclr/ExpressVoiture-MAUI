using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressVoiture.Domain.Models
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
