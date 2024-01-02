using Core.Interfaces;
using Core.Models;
using Data.Data;
using Data.Services;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Employee = new GenricService<Employee>(_context);
            Customer = new GenricService<Customer>(_context);
            Product = new GenricService<Product>(_context);
        }
        public IRepository<Employee> Employee { get; set; }
        public IRepository<Customer> Customer { get; set; }
        public IRepository<Product> Product { get; set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
