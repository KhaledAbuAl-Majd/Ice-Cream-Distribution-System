namespace Ice_Cream_Distribution_System.Models;

public partial class Car
{
    public short Id { get; private set; }

    public int AreaId { get; set; }

    public string? CarDetails { get; set; }

    public virtual Area Area { get; set; } = null!;

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Representative> Representatives { get; set; } = new List<Representative>();

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}
