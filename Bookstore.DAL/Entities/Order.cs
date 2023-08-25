namespace Bookstore.DAL.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public decimal Cost { get; set; }
    public List<Book> Books { get; set; } = new();
    public List<BookOrder> BookOrders { get; set; } = new();
}
