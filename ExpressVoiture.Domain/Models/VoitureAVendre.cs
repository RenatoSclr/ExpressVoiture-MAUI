using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressVoiture.Domain.Models
{
    public class VoitureAVendre
    {
        [Key]
        public int VoitureId { get; set; }

        public int Annee { get; set; }

        public string? Marque{ get; set; }

        public string? Modele{ get; set; }

        public string? Finition { get; set; }

        public DateTime DateAchat { get; set; }

        public double PrixAchat { get; set; }
        
        public string? CodeVIN { get; set; }

        public string? ImagePath { get; set; }

        public Reparation? Reparation { get; set; }
        public Vente? Vente { get; set; }
    }
}
