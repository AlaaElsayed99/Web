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
    public class CustomerService : ICustomer
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Customer>> GetAll()
        {
            var cusomers=await _context.Customers.ToListAsync();
            return cusomers;
        }
    }
}
