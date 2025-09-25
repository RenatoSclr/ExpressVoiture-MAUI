using ExpressVoiture.API.Domain.Models;
using ExpressVoiture.API.Infrastruture.Data;
using ExpressVoiture.API.Infrastruture.Repository;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoiture.Tests.IntegrationsTests
{
    public class DatabaseFixture : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ExpressVoitureTestDatabase")
                .Options;

            _dbContext = new ApplicationDbContext(options);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
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
            return _dbContext.Voitures
                .Include(v => v.Reparation)
                .Include(v => v.Vente)
                .FirstOrDefault(v => v.VoitureId == id);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
