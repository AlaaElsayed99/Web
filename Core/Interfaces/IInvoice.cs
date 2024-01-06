
using Core.Models;
using Core.ViewModels;

namespace Core.Interfaces
{
    public interface IInvoice
    {
        Task CreateAsync(InvoiceVM Vm);
        Task<List<Invoice>> SearchInvoice(ParamsVm paramsVm);
        Task<Invoice> GetByIdIncludes(int id);
    }
}
