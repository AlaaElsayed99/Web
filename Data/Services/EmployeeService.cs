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
    public class EmployeeService : IEmployee
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Employee>> GetAll()
        {
            var Employees = await _context.Employees.ToListAsync();
            return Employees;
        }
    }
}
