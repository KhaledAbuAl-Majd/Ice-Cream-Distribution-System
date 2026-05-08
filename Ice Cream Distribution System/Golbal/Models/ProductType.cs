using System;
using System.Collections.Generic;

namespace Ice_Cream_Distribution_System.Models;

public partial class ProductType
{
    public short Id { get; private set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
