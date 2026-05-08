using System;
using System.Collections.Generic;

namespace Ice_Cream_Distribution_System.Models;

public partial class Payment
{
    public int Id { get; private set; }

    public decimal PayedValue { get; set; }

    public DateTime? Date { get; set; }

    public int RepresentativeId { get; set; }

    public int StoreId { get; set; }

    public string? Notes { get; set; }

    public virtual Representative Representative { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
