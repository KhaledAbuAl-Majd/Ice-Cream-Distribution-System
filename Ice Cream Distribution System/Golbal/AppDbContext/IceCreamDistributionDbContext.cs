using Ice_Cream_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Golbal.AppDbContext;

public partial class IceCreamDistributionDbContext : DbContext
{
    public IceCreamDistributionDbContext()
    {
    }

    public IceCreamDistributionDbContext(DbContextOptions<IceCreamDistributionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceRecord> InvoiceRecords { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Representative> Representatives { get; set; }

    public virtual DbSet<RepresentativesStock> RepresentativesStocks { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=IceCreamDistributionDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Areas__3214EC27171E31AC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Notes).HasMaxLength(500);
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cars__3214EC27A292FF42");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AreaId).HasColumnName("AreaID");
            entity.Property(e => e.CarDetails).HasMaxLength(500);

            entity.HasOne(d => d.Area).WithMany(p => p.Cars)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK__Cars__AreaID__4222D4EF");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drivers__3214EC270252AD56");

            entity.HasIndex(e => e.UserId, "UQ__Drivers__1788CCADEAFB7C1C").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Car).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.CarId)
                .HasConstraintName("FK__Drivers__CarID__5535A963");

            entity.HasOne(d => d.User).WithOne(p => p.Driver)
                .HasForeignKey<Driver>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Drivers__UserID__5441852A");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Invoices__3214EC277A90C751");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 4)");

            entity.HasOne(d => d.Car).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__CarID__0E6E26BF");

            entity.HasOne(d => d.Store).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__StoreI__0F624AF8");
        });

        modelBuilder.Entity<InvoiceRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InvoiceR__3214EC27CF6C5061");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("Insert_Update_DeleteInvoiceRecords");
                    tb.HasTrigger("trg_CalcualteStoresBalance_Insert_Update_DeleteInvoiceRecords");
                });

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductPrice).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Total)
                .HasComputedColumnSql("([Count]*[ProductPrice])", true)
                .HasColumnType("decimal(24, 4)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceRecords)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__InvoiceRe__Invoi__1332DBDC");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceRecords)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceRe__Produ__14270015");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC2751AE37D3");

            entity.ToTable(tb => tb.HasTrigger("trg_CalcualteStoresBalance_Insert_Update_DeletePayments"));

            entity.HasIndex(e => e.RepresentativeId, "IX_Payments_Representative");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.PayedValue).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.RepresentativeId).HasColumnName("RepresentativeID");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");

            entity.HasOne(d => d.Representative).WithMany(p => p.Payments)
                .HasForeignKey(d => d.RepresentativeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Repres__6754599E");

            entity.HasOne(d => d.Store).WithMany(p => p.Payments)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__StoreI__68487DD7");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__People__AA2FFB854B41C649");

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PersonName).HasMaxLength(200);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC27E05FA1C5");

            entity.HasIndex(e => e.Name, "IX_Products_Name");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Produc__44FF419A");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductT__3214EC27BB1856AB");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<Representative>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Represen__3214EC27ED28EF31");

            entity.HasIndex(e => e.UserId, "UQ__Represen__1788CCADCA5AF74E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Car).WithMany(p => p.Representatives)
                .HasForeignKey(d => d.CarId)
                .HasConstraintName("FK__Represent__CarID__5070F446");

            entity.HasOne(d => d.User).WithOne(p => p.Representative)
                .HasForeignKey<Representative>(d => d.UserId)
                .HasConstraintName("FK__Represent__UserI__4F7CD00D");
        });

        modelBuilder.Entity<RepresentativesStock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Represen__3214EC2758213D3F");

            entity.ToTable("RepresentativesStock");

            entity.HasIndex(e => new { e.RepresentativeId, e.ProductId }, "IX_RepStock_Rep_Product");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.RepresentativeId).HasColumnName("RepresentativeID");

            entity.HasOne(d => d.Product).WithMany(p => p.RepresentativesStocks)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Represent__Produ__6B24EA82");

            entity.HasOne(d => d.Representative).WithMany(p => p.RepresentativesStocks)
                .HasForeignKey(d => d.RepresentativeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Represent__Repre__6C190EBB");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shifts__3214EC2764D6B26F");

            entity.HasIndex(e => new { e.FromDate, e.ToDate }, "IX_Shifts_Dates");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.RepresentativeId).HasColumnName("RepresentativeID");

            entity.HasOne(d => d.Car).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Shifts__CarID__59FA5E80");

            entity.HasOne(d => d.Driver).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Shifts__DriverID__59063A47");

            entity.HasOne(d => d.Representative).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.RepresentativeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Shifts__Represen__5812160E");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stores__3214EC27F186817C");

            entity.HasIndex(e => e.AreaId, "IX_Stores_Area");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AreaId).HasColumnName("AreaID");
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

            entity.HasOne(d => d.Area).WithMany(p => p.Stores)
                .HasForeignKey(d => d.AreaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stores__AreaID__3E52440B");

            entity.HasOne(d => d.Owner).WithMany(p => p.Stores)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stores__OwnerID__3F466844");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC83799C9E");

            entity.HasIndex(e => e.PersonId, "IX_Users_PersonID")
                .IsUnique()
                .HasFilter("([IsDeleted]=(0))");

            entity.HasIndex(e => e.UserName, "IX_Users_UserName").IsUnique();

            entity.HasIndex(e => e.PersonId, "UQ__Users__AA2FFB84EECC5C9C").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456322FAA8B").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(100);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Person).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__PersonID__49C3F6B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
