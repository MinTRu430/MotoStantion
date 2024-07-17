using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<RepairOrder> RepairOrders { get; set; }
    public DbSet<Part> Parts { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Конфигурация связи один ко многим между Customer и Motorcycle
        modelBuilder.Entity<Motorcycle>()
            .HasOne(m => m.Customer)
            .WithMany(c => c.Motorcycles)
            .HasForeignKey(m => m.CustomerId);

        // Конфигурация связи один ко многим между Employee и RepairOrder
        modelBuilder.Entity<RepairOrder>()
            .HasOne(ro => ro.Employee)
            .WithMany()
            .HasForeignKey(ro => ro.EmployeeId);

        // Конфигурация связи один ко многим между Motorcycle и RepairOrder
        modelBuilder.Entity<RepairOrder>()
            .HasOne(ro => ro.Motorcycle)
            .WithMany()
            .HasForeignKey(ro => ro.MotorcycleId);
    }
}