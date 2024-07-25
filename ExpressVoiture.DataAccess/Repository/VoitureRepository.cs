using ExpressVoiture.DataAccess.Data;
using ExpressVoiture.Domain.IRepository;
using ExpressVoiture.Domain.Models;

namespace ExpressVoiture.DataAccess.Repository
{
    public class VoitureRepository : Repository<VoitureAVendre>, IVoitureRepository
    {
        private ApplicationDbContext _context;
        public VoitureRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(VoitureAVendre voiture)
        {
            _context.Update(voiture);
        }
    }
}
