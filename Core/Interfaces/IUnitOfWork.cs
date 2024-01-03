using Core.Models;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customer { get; }
        IRepository<Employee> Employee { get; }
        IRepository<Product> Product { get; }
        IRepository<Invoice> Invoice { get; }
        IRepository<InvoiceItem> InvoiceItems { get; }

        int Complete();

    }
}
