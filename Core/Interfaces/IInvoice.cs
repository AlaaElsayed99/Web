
using Core.ViewModels;

namespace Core.Interfaces
{
    public interface IInvoice
    {
        Task CreateAsync(InvoiceVM Vm);
    }
}
