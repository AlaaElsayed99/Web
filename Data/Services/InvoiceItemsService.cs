using Core.Interfaces;
using Core.Models;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class InvoiceItemsService : IInvoiceItem
    {
        private readonly AppDbContext _context;

        public InvoiceItemsService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Invoice vm)
        {
            throw new NotImplementedException();
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InvoiceItem>> GetAll()
        {
            var Items = await _context.InvoiceItems.ToListAsync();
            return Items;
        }

        public Invoice GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

        public List<Invoice> Search(string name)
        {
            throw new NotImplementedException();
        }
    }
}
