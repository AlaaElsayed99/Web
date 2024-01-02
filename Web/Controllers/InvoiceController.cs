using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceItem _invoice;

        public InvoiceController(ILogger<InvoiceController> logger, IUnitOfWork unitOfWork, IInvoiceItem invoice)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

            _invoice = invoice;
        }

        public IActionResult Index()
        {
            return View();
        }
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