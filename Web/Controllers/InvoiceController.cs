using Core.Interfaces;
using Core.Models;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Web.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoice _invoice;

        public InvoiceController(ILogger<InvoiceController> logger, IUnitOfWork unitOfWork, IInvoice invoice)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _invoice = invoice;
        }

        public async Task<IActionResult> Index()
        {
            var Invoices = await _unitOfWork.Invoice.GetAllAsync(new List<string> { "customer", "employee" });
            return View(Invoices);
        }
        [HttpGet]
        public async Task<IActionResult> CreateInvoice()
        {
            var customers = await _unitOfWork.Customer.GetAllAsync();
            var employees = await _unitOfWork.Employee.GetAllAsync();
            var products = await _unitOfWork.Product.GetAllAsync();
            var InvoiceVm = new InvoiceVM
            {
                Customers = customers,
                Employees = employees,
                Products = products
            };
            InvoiceVm.Items.Add(new InvoiceItem { Quantity = 1, });
            return View(InvoiceVm);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateNewInvoice(InvoiceVM invoiceVM)
        {
            if (invoiceVM is null)
            {
                var customers = await _unitOfWork.Customer.GetAllAsync();
                var employees = await _unitOfWork.Employee.GetAllAsync();
                var products = await _unitOfWork.Product.GetAllAsync();
                var InvoiceVm = new InvoiceVM
                {
                    Customers = customers,
                    Employees = employees,
                    Products = products
                };
                InvoiceVm.Items.Add(new InvoiceItem { Quantity = 1, });
                return View("CreateInvoice", InvoiceVm);
            }
            //return View("CreateInvoice", invoiceVM);
            //if (!ModelState.IsValid)
            //{
            using (var transaction = new TransactionScope())
            {
                try
                {
                    await _invoice.CreateAsync(invoiceVM);
                    _unitOfWork.Complete();
                    transaction.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    throw ex;
                }
            }

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return View("Error");

            var invoice = await _unitOfWork.Invoice.GetByIdAsync(i => i.Id == id);

            if (invoice == null)
                return NotFound("Not Found"); var invoincesItems = await _unitOfWork.InvoiceItems.GetByIdAsync(i => i.InvoiceId == id);
            await _unitOfWork.InvoiceItems.DeleteAsync(invoincesItems);
            await _unitOfWork.Invoice.DeleteAsync(invoice);
            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var invoice = await _unitOfWork.Invoice.GetByIdAsync(s => s.Id == id, new List<string> { "customer", "employee", "Items" });
            return View(invoice);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewItem(InvoiceVM viewModel)
        {
            viewModel.Items.Add(new InvoiceItem() { Quantity = 1 }); // Add a new empty item
            viewModel.Products = await _unitOfWork.Product.GetAllAsync();
            return PartialView("_AddNewItem", viewModel);
        }
        [HttpPost]
        public async Task<ActionResult> RemoveItem(InvoiceVM viewModel, int itemId)
        {
            viewModel.Products = await _unitOfWork.Product.GetAllAsync();
            viewModel.Items.RemoveAt(itemId);
            return PartialView("_AddNewItem", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Search(ParamsVm paramsVm)
        {
            var invoicesQuery = _unitOfWork.Invoice.GetAllAsync(new List<string> { "customer", "employee" });

            if (!string.IsNullOrEmpty(paramsVm.customerName))
            {
                invoicesQuery = invoicesQuery.Where(i => i.customer.Name.Contains(paramsVm.customerName));
            }

            if (!string.IsNullOrEmpty(paramsVm.employeeName))
            {
                invoicesQuery = invoicesQuery.Where(i => i.employee.Name.Contains(paramsVm.employeeName));
            }

            if (paramsVm.from != null)
            {
                invoicesQuery = invoicesQuery.Where(i => i.CreatedAt >= paramsVm.from);
            }

            if (paramsVm.to != null)
            {
                invoicesQuery = invoicesQuery.Where(i => i.CreatedAt <= paramsVm.to);
            }

            var invoices = await invoicesQuery.ToListAsync();

            var indexVMList = invoices.Select(invoice => new IndexVM
            {
                Id = invoice.Id,
                CustomerId = invoice.CustomerId,
                Customer = invoice.customer,
                EmployeeId = invoice.EmployeeId,
                Employee = invoice.employee,
                Items = invoice.Items,
                CreatedAt = invoice.CreatedAt
            }).ToList();

            ViewBag.Params = paramsVm;

            return View("Index", indexVMList);
        }

    }
}