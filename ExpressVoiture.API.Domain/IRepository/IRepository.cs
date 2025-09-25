using System.Linq.Expressions;

namespace ExpressVoiture.API.Domain.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(string includeProperties = null);
        Task<T> Get(Expression<Func<T, bool>> filter, string includeProperties = null);
        Task Add(T entity);
        Task Remove(T entity);
    }
}
