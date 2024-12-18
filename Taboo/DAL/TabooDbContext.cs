using Microsoft.EntityFrameworkCore;
using Taboo.Entities;

namespace Taboo.DAL;

public class TabooDbContext : DbContext
{
    public TabooDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Language> Languages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Language
        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(x => x.Code);
            entity.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(2);
            entity.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(32);
            entity.Property(x => x.Icon)
            .IsRequired();
        });
        base.OnModelCreating(modelBuilder);
    }
}
