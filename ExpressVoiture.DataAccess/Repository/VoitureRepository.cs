using ExpressVoiture.DataAccess.Data;
using ExpressVoiture.Domain;
using ExpressVoiture.Domain.IRepository;
using ExpressVoiture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
