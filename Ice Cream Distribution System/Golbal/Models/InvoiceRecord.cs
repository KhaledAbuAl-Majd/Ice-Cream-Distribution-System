using System;
using System.Collections.Generic;

namespace Ice_Cream_Distribution_System.Models;

public partial class InvoiceRecord
{
    public int Id { get; private set; }

    public int InvoiceId { get; set; }

    public int ProductId { get; set; }

    public short Count { get; set; }

    public decimal ProductPrice { get; set; }

    public decimal? Total { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
