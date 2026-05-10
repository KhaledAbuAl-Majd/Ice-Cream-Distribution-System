namespace Ice_Cream_Distribution_System.Models;

public partial class Representative
{
    public int Id { get; private set; }

    public int UserId { get; set; }

    public short CarId { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<RepresentativesStock> RepresentativesStocks { get; set; } = new List<RepresentativesStock>();

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();

    public virtual User User { get; set; } = null!;
}
