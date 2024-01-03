using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Core.ViewModels;
using System.Transactions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        public async Task<IActionResult> Delete (int  id)
        {
            if (id == 0)
                return View("Error");

            var invoice = await _unitOfWork.Invoice.GetByIdAsync(i => i.Id == id);

            if (invoice == null)
                return NotFound("Not Found");
            var invoincesItems = await _unitOfWork.InvoiceItems.GetByIdAsync(i => i.InvoiceId == id);
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
    }
}