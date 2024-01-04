using Core.Interfaces;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Services
{
    public class GenricService<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenricService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public async Task<IEnumerable<T>> GetAllAsync(List<string> includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate, List<string> includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> Queryable()
        {
            throw new NotImplementedException();
        }

        public int SaveAsync()
        {
            
            return _context.SaveChanges();
        }

        public T UpdateAsync(T entity)
        {
            _context.Update(entity);
            return entity;
        }
    }
}
