using ExpressVoiture.DataAccess.Data;
using ExpressVoiture.DataAccess.Repository;
using ExpressVoiture.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoiture.Tests.IntegrationsTests
{
    public class DatabaseFixture : IDisposable
    {
        private readonly string _connectionString = "Server=.;Database=ExpressVoitureTestDatabase;Trusted_Connection=True;TrustServerCertificate=True;";
        private readonly ApplicationDbContext _dbContext;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_connectionString)
            .Options;

            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.Migrate();
        }

        public void AddVehicle(VoitureAVendre product)
        {
            var repository = new VoitureRepository(_dbContext);
            repository.Add(product);
            repository.Save();
        }

        public void DeleteVehicle(VoitureAVendre voiture)
        {
            var repository = new VoitureRepository(_dbContext);
            repository.Remove(voiture);
            repository.Save();
        }

        public void UpdateVehicle(VoitureAVendre voiture)
        {
            var repository = new VoitureRepository(_dbContext);
            repository.Update(voiture);
            repository.Save();
        }

        public VoitureAVendre GetVehicle(int id)
        {
            return _dbContext.Voitures.FirstOrDefault(v => v.VoitureId == id);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
