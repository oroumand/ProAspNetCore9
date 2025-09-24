using Microsoft.EntityFrameworkCore;
using ShortUrl.LinkManager.API.Domain;

namespace ShortUrl.LinkManager.API.DAL;

public class LinkDbContext : DbContext
{
    public LinkDbContext(DbContextOptions<LinkDbContext> options) : base(options) { }

    public DbSet<Link> Links => Set<Link>();
    public DbSet<ClickEvent> ClickEvents => Set<ClickEvent>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Link>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Code).IsUnique();
            e.Property(x => x.Code).IsRequired().HasMaxLength(64);
            e.Property(x => x.LongUrl).IsRequired().HasMaxLength(2048);
            e.Property(x => x.Status).HasConversion<string>().IsRequired();
        });

        modelBuilder.Entity<ClickEvent>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Code);                // جستجوی سریع با code
            e.HasIndex(x => new { x.LinkId, x.Ts }); // گزارش‌های زمانی
            e.Property(x => x.Code).HasMaxLength(64).IsRequired();
            e.Property(x => x.Ip).HasMaxLength(64);
            e.Property(x => x.UserAgent).HasMaxLength(1024);
            e.Property(x => x.Referrer).HasMaxLength(2048);
        });
    }
}
