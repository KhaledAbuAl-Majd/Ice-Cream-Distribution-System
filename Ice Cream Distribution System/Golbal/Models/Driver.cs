namespace Ice_Cream_Distribution_System.Models;

public partial class Driver
{
    public int Id { get; private set; }

    public int UserId { get; set; }

    public short CarId { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();

    public virtual User User { get; set; } = null!;
}
