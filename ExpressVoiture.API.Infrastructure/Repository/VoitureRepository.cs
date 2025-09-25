using ExpressVoiture.API.Domain.IRepository;
using ExpressVoiture.API.Domain.Models;
using ExpressVoiture.API.Infrastruture.Data;

namespace ExpressVoiture.API.Infrastruture.Repository
{
    public class VoitureRepository : Repository<VoitureAVendre>, IVoitureRepository
    {
        private ApplicationDbContext _context;
        public VoitureRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(VoitureAVendre voiture)
        {
            _context.Update(voiture);
        }
    }
}
