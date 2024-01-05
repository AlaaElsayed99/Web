using Core.Interfaces;
using Core.Models;
using Core.ViewModels;
using Data.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Invoice>> SearchInvoice(ParamsVm paramsVm)
        {
            var query = _context.Invoices.Include(i => i.customer)
                .Include(i => i.employee).Include(i => i.Items).AsQueryable();


            if (!string.IsNullOrEmpty(paramsVm.customerName))
            {
                query = query.Where(i => i.customer.Name.Contains(paramsVm.customerName.ToLower()));
            }

            if (!string.IsNullOrEmpty(paramsVm.employeeName))
            {
                query = query.Where(i => i.employee.Name.Contains(paramsVm.employeeName.ToLower()));
            }

            if (paramsVm.from != null)
            {
                query = query.Where(i => i.CreatedAt >= paramsVm.from);
            }

            if (paramsVm.to != null)
            {
                query = query.Where(i => i.CreatedAt <= paramsVm.to);
            }
            var invoices = query.ToList();
            return invoices;
        }


    }
}

