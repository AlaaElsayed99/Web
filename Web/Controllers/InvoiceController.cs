using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ViewModels;

namespace Web.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IEmployee _employee;
        private readonly ICustomer _customer;
        private readonly IInvoiceItem _invoice;

        public InvoiceController(ILogger<InvoiceController> logger, IEmployee employee, ICustomer customer, IInvoiceItem invoice)
        {
            _logger = logger;
            _employee = employee;
            _customer = customer;
            _invoice = invoice;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateInvoice()
        {
            var customers = await _customer.GetAll();
            var employees = await _employee.GetAll();
            var invoiceItems = await _invoice.GetAll();
            ViewBag.CustomerList = new SelectList(customers, "Id", "Name");
            ViewBag.EmployeeList = new SelectList(employees, "Id", "Name");
            ViewBag.InvoiceItems = new SelectList(invoiceItems, "Id", "Name");
            var InvoiceVm = new InvoiceVM();
            return View(InvoiceVm);
        }
    }
}
