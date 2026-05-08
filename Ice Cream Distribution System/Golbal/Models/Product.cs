using System;
using System.Collections.Generic;

namespace Ice_Cream_Distribution_System.Models;

public partial class Product
{
    public int Id { get; private set; }

    public string Name { get; set; } = null!;

    public short ProductTypeId { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<InvoiceRecord> InvoiceRecords { get; set; } = new List<InvoiceRecord>();

    public virtual ProductType ProductType { get; set; } = null!;

    public virtual ICollection<RepresentativesStock> RepresentativesStocks { get; set; } = new List<RepresentativesStock>();
}
