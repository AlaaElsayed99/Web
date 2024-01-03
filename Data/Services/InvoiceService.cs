using Core.Interfaces;
using Core.Models;
using Core.ViewModels;
using Data.Data;

namespace Data.Services
{
    public class InvoiceService : IInvoice
    {
        private readonly AppDbContext _context;

        public InvoiceService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(InvoiceVM Vm)
        {


            var Invoice = new Invoice
            {
                CreatedAt = Vm.CreatedAt,
                CustomerId = Vm.CustomerId,
                EmployeeId = Vm.EmployeeId,
                Items = Vm.Items,
            };
            await _context.AddAsync(Invoice);
        }
    }
}

