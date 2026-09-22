using System;
using System.Collections.Generic;

namespace reactSyncfusionApp.Models;

public partial class Order
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

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<OrderTotal> OrderTotals { get; set; } = new List<OrderTotal>();
}
