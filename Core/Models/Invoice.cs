namespace Core.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer? customer { get; set; }
        public int EmployeeId { get; set; }
        public Employee? employee { get; set; }
       
        public List<InvoiceItem>? Items { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
