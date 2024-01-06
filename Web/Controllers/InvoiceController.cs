using Core.Interfaces;
using Core.Models;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Transactions;

namespace Web.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoice _invoice;
        private readonly IToastNotification _toast;

        public InvoiceController(ILogger<InvoiceController> logger, IUnitOfWork unitOfWork, IInvoice invoice,
            IToastNotification toast)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _invoice = invoice;
            _toast = toast;
        }

        public async Task<IActionResult> Index(int pageSize = 5, int pageNumber = 1)
        {
            var invoices = await _unitOfWork.Invoice
                .GetAllAsync(new List<string> { "customer", "employee" }, pageSize: pageSize, pageNumber: pageNumber);


            return View(invoices.Select(invoice => new IndexVM
            {
                Id = invoice.Id,
                customer = invoice.customer, // Implement a mapping method for CustomerViewModel
                employee = invoice.employee, // Implement a mapping method for EmployeeViewModel
                Items = invoice.Items,
                CreatedAt = invoice.CreatedAt,
                PageNumber = pageNumber,
                PageSize = pageSize
            }).ToList());
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
            InvoiceVm.Items.Add(new InvoiceItem { Quantity = 1 });

            return View(InvoiceVm);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateNewInvoice(InvoiceVM invoiceVM)
        {

            if (ModelState.IsValid)
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        await _invoice.CreateAsync(invoiceVM);
                        _unitOfWork.Complete();
                        transaction.Complete();
                        _toast.AddSuccessToastMessage("Invoice create success");
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        transaction.Dispose();
                        _toast.AddErrorToastMessage("Invoice error");

                    }
                }
            }
            var customers = await _unitOfWork.Customer.GetAllAsync();
            var employees = await _unitOfWork.Employee.GetAllAsync();
            var products = await _unitOfWork.Product.GetAllAsync();

            invoiceVM.Customers = customers;
            invoiceVM.Employees = employees;
            invoiceVM.Products = products;
            return View("CreateInvoice", invoiceVM);


        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return View("Error");

            var invoice = await _unitOfWork.Invoice.GetByIdAsync(i => i.Id == id);

            if (invoice == null)
                return NotFound("Not Found");
            var invoincesItems =
                await _unitOfWork.InvoiceItems.GetAllAsyncWhere(i => i.InvoiceId == id);
            foreach (var item in invoincesItems)
            {
                await _unitOfWork.InvoiceItems.DeleteAsync(item);
            }

            await _unitOfWork.Invoice.DeleteAsync(invoice);
            _unitOfWork.Complete();
            _toast.AddSuccessToastMessage("Invoice Deleted");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var invoice = await _invoice.GetByIdIncludes(id);
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
            //var items = viewModel.Items;
            var items = new List<InvoiceItem>(viewModel.Items); // Create a copy

            items.RemoveAt(itemId);
            viewModel.Items = items;
            return PartialView("_AddNewItem", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Search(ParamsVm paramsVm)
        {
            var invoices = await _invoice.SearchInvoice(paramsVm);
            var indexVMList = invoices.Select(invoice => new IndexVM
            {
                Id = invoice.Id,
                CustomerId = invoice.CustomerId,
                customer = invoice.customer,
                EmployeeId = invoice.EmployeeId,
                employee = invoice.employee,
                Items = invoice.Items,
                CreatedAt = invoice.CreatedAt
            }).ToList();
            return PartialView("_ReportInvoce", indexVMList);
        }

    }
}