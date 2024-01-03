using Core.Models;

namespace Core.ViewModels
{
    public class InvoiceVM
    {
        public int? Id { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public List<InvoiceItem> Items { get; set; }

        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<Employee>? Employees { get; set; }
        public IEnumerable<Customer>? Customers { get; set; }

        public DateTime CreatedAt { get; set; }
        public InvoiceVM()
        {
            Items = new List<InvoiceItem>();

        }


    }
}
