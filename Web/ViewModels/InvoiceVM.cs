using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class InvoiceVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public int InvoiceItemId { get; set; }

        public List<InvoiceItem> Items { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }

        public InvoiceVM()
        {
            Items = new List<InvoiceItem>();
        }


    }
}
