using ExpressVoiture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressVoiture.Domain.IRepository
{
    public interface IVoitureRepository : IRepository<VoitureAVendre>
    {
        void Update(VoitureAVendre voiture);
        void Save();
    }
}

