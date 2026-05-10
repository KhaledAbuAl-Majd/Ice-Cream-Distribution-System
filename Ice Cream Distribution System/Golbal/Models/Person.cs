using System;
using System.Collections.Generic;

namespace Ice_Cream_Distribution_System.Models;

public partial class Person
{
    public int PersonId { get; private set; }

    public string PersonName { get; set; } = null!;

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();

    public virtual User? User { get; set; }
}
