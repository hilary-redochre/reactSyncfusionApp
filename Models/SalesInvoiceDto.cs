namespace reactSyncfusionApp.Models;

public class SalesInvoiceDto
{
    public int OrderId { get; set; }

    public string? CustomerName { get; set; }
    public string? ShipAddress { get; set; }
    public string? ShipCity { get; set; }
    public string? ShipPostalCode { get; set; }
    public string? ShipCountry { get; set; }

    public DateOnly? OrderDate { get; set; }
    public int? CustomerId { get; set; }
    public DateOnly? RequiredDate { get; set; }
    public DateOnly? ShippedDate { get; set; }

    public List<SalesInvoiceDetailDto> Details { get; set; } = new();

    public decimal? Subtotal { get; set; }
    public decimal? Freight { get; set; }
    public decimal? Total { get; set; }
}
