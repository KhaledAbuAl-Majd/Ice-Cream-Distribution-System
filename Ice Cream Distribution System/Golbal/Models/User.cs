using System;
using System.Collections.Generic;

namespace Ice_Cream_Distribution_System.Models;

public partial class User
{
    public int UserId { get; private set; }

    public string UserName { get; set; } = null!;

    public int PersonId { get; set; }

    public string PasswordHash { get; set; } = null!;

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual Representative? Representative { get; set; }
}
