namespace Core.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public List<InvoiceItem> Items { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
