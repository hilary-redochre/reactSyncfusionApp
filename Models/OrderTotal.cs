using System;
using System.Collections.Generic;

namespace reactSyncfusionApp.Models;

public partial class OrderTotal
{
    public int OrderTotalId { get; set; }

    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Freight { get; set; }

    public decimal? Total { get; set; }

    public virtual Order Order { get; set; } = null!;
}
