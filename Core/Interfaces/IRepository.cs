using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(List<string> includes = null);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate, List<string> includes = null);
        Task<T> CreateAsync(T entity);
        T UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        int SaveAsync();
    }
}
