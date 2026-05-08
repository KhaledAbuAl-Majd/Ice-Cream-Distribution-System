using System;
using System.Collections.Generic;

namespace Ice_Cream_Distribution_System.Models;

public partial class Area
{
    public int Id { get; private set; }

    public string Name { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
