using Microsoft.EntityFrameworkCore;
using ShortUrl.CodeGenerator.API.Domain;

namespace ShortUrl.CodeGenerator.API.DAL;




public class CodeDbContext : DbContext
{
    public CodeDbContext(DbContextOptions<CodeDbContext> options) : base(options) { }
    public CodeDbContext()
    {
        
    }
    public DbSet<ShortCode> ShortCodes => Set<ShortCode>();
    public DbSet<AllocationLedger> AllocationLedgers => Set<AllocationLedger>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortCode>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Value).IsUnique();
            e.Property(x => x.Value).IsRequired().HasMaxLength(64);
            e.Property(x => x.Length).IsRequired();
            e.Property(x => x.Status).HasConversion<string>().IsRequired();
        });

        modelBuilder.Entity<AllocationLedger>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => new { x.RequesterService, x.DeliveredAt });
            e.Property(x => x.RequesterService).HasMaxLength(128);
        });
    }
}
