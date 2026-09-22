using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reactSyncfusionApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SalesInvoiceDbContext _context;

        public OrdersController(SalesInvoiceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();

            return Ok(orders);
        }
    }
}
