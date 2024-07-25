using ExpressVoiture.DataAccess.Data;
using ExpressVoiture.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace ExpressVoiture.DataAccess.Repository
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
        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.Where(filter).FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
    }
}
