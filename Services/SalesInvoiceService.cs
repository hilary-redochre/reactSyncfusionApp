using Microsoft.EntityFrameworkCore;
using reactSyncfusionApp.Models;

namespace reactSyncfusionApp.Services;

public class SalesInvoiceService
{
    private readonly SalesInvoiceDbContext _context;

    public SalesInvoiceService(SalesInvoiceDbContext context)
    {
        _context = context;
    }

    public async Task<SalesInvoiceDto?> GetInvoice(int orderId)
    {
        // retrieve Order
        var order = await _context.Orders
       .FirstOrDefaultAsync(x => x.OrderId == orderId);

        if (order == null)
            return null;
        // retrieve OrderDetails
        var details = await _context.OrderDetails
       .Where(x => x.OrderId == orderId)
       .ToListAsync();

        // retrieve OrderTotal
        var total = await _context.OrderTotals
            .FirstOrDefaultAsync(x => x.OrderId == orderId);

        // create SalesInvoiceDto
        var invoice = new SalesInvoiceDto
        {
            OrderId = order.OrderId,
            CustomerName = order.CustomerName,
            ShipAddress = order.ShipAddress,
            ShipCity = order.ShipCity,
            ShipPostalCode = order.ShipPostalCode,
            ShipCountry = order.ShipCountry,

            OrderDate = order.OrderDate,
            CustomerId = order.CustomerId,
            RequiredDate = order.RequiredDate,
            ShippedDate = order.ShippedDate,

            Subtotal = total?.Subtotal,
            Freight = total?.Freight,
            Total = total?.Total,

            Details = details.Select(x => new SalesInvoiceDetailDto
            {
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                Discount = x.Discount,
                ExtendedPrice = x.ExtendedPrice
            }).ToList()
        };

        // return it
        return invoice;
       
    }
}