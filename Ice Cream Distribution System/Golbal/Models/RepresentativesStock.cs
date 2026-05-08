using System;
using System.Collections.Generic;

namespace Ice_Cream_Distribution_System.Models;

public partial class RepresentativesStock
{
    public int Id { get; private set; }

    public int ProductId { get; set; }

    public int RepresentativeId { get; set; }

    public short? Count { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Representative Representative { get; set; } = null!;
}
