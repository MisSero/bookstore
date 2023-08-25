using Microsoft.EntityFrameworkCore;
using Bookstore.Domain.Entities;

namespace Bookstore.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // configure decimal precision and for datetime storage only date
        modelBuilder.Entity<Book>().Property(b => b.Released).HasColumnType("date");
        modelBuilder.Entity<Book>().Property(b => b.Price).HasPrecision(12, 2);

        modelBuilder.Entity<Order>().Property(o => o.Cost).HasPrecision(12, 2);

        // explicitly adding an entity to set Id key
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Orders)
            .WithMany(o => o.Books)
            .UsingEntity<BookOrder>();
    }
}
