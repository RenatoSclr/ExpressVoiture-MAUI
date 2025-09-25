using ExpressVoiture.API.Domain.Models;

namespace ExpressVoiture.API.Domain.IRepository
{
    public interface IVoitureRepository : IRepository<VoitureAVendre>
    {
        Task Update(VoitureAVendre voiture);
        Task Save();
    }
}

