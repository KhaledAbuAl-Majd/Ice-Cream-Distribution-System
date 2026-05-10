using System;
using System.Collections.Generic;

namespace Ice_Cream_Distribution_System.Models;

public partial class Shift
{
    public int Id { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public int RepresentativeId { get; set; }

    public int DriverId { get; set; }

    public short CarId { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual Driver Driver { get; set; } = null!;

    public virtual Representative Representative { get; set; } = null!;
}
