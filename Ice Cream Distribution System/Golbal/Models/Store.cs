using System;
using System.Collections.Generic;

namespace Ice_Cream_Distribution_System.Models;

public partial class Store
{
    public int Id { get; private set; }

    public decimal Balance { get; private set; }

    public int AreaId { get; set; }

    public int OwnerId { get; set; }

    public virtual Area Area { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual Person Owner { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
