namespace reactSyncfusionApp.Models
{
    public class SalesInvoiceDetailDto
    {
        public string? ProductName { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? Quantity { get; set; }

        public decimal? Discount { get; set; }

        public decimal? ExtendedPrice
        {
            get; set;
        }

    }
}
