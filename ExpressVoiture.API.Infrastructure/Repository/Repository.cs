using ExpressVoiture.API.Domain.IRepository;
using ExpressVoiture.API.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace ExpressVoiture.API.Infrastruture.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> DbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.DbSet = _context.Set<T>();
        }
        public async Task Add(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter, string includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll(string includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.ToListAsync();
        }

        public async Task Remove(T entity)
        {
            DbSet.Remove(entity);
        }
    }
}
