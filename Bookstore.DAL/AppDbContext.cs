using Microsoft.EntityFrameworkCore;
using Bookstore.Domain.Entities;

namespace Bookstore.DAL;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().Property(b => b.Released).HasColumnType("date");
        modelBuilder.Entity<Book>().Property(b => b.Price).HasPrecision(12, 2);

        modelBuilder.Entity<Order>().Property(o => o.Cost).HasPrecision(12, 2);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Orders)
            .WithMany(o => o.Books)
            .UsingEntity<BookOrder>();
    }
}
