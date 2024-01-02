using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IInvoiceItem
    {
        Task<IEnumerable<InvoiceItem>> GetAll();
        Invoice GetById(int? id);
        void Delete(int? id);
        void save();
        void Add(Invoice vm);
        List<Invoice> Search(string name);
    }
}
