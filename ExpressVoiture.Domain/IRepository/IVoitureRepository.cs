using ExpressVoiture.Domain.Models;

namespace ExpressVoiture.Domain.IRepository
{
    public interface IVoitureRepository : IRepository<VoitureAVendre>
    {
        void Update(VoitureAVendre voiture);
        void Save();
    }
}

