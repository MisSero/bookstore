namespace Bookstore.DAL.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime Released { get; set; }
    public decimal Price { get; set; }
    public List<Order> Orders { get; set; } = new();
    public List<BookOrder> BookOrders { get; set; } = new();
}
