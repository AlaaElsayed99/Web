using Core.Models;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customer { get; }
        IRepository<Employee> Employee { get; }
        IRepository<Product> Product { get; }
        int Complete();

    }
}
