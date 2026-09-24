using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reactSyncfusionApp.Models;
using reactSyncfusionApp.Services;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SalesInvoiceDbContext _context;

        private readonly SalesInvoiceService _salesInvoiceService;


        public OrdersController(SalesInvoiceDbContext context, SalesInvoiceService salesInvoiceService)
        {
            _context = context;
            _salesInvoiceService = salesInvoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();

            return Ok(orders);
        }

        [HttpGet("{orderId}/invoice")]
        public async Task<IActionResult> GetInvoice(int orderId)
        {
            var invoice = await _salesInvoiceService.GetInvoice(orderId);

            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }
    }
}
