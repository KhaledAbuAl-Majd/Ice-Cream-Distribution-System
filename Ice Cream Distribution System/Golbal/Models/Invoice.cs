using System;
using System.Collections.Generic;

namespace Ice_Cream_Distribution_System.Models;

public partial class Invoice
{
    public int Id { get; private set; }

    public DateTime? Date { get; set; }

    public short CarId { get; set; }

    public int StoreId { get; set; }

    public string? Notes { get; set; }

    public decimal? Total { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual ICollection<InvoiceRecord> InvoiceRecords { get; set; } = new List<InvoiceRecord>();

    public virtual Store Store { get; set; } = null!;
}
