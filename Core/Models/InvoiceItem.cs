using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class InvoiceItem
    {
        public int ProductId { get; set; }
        public int InvoiceId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [NotMapped]
        public decimal TotalPrice { get; set; }

    }
}
