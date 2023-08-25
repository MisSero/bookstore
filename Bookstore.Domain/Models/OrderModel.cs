namespace Bookstore.Domain.Models;

public class OrderModel
{
    public int Number { get; set; }
    public DateTime Created { get; set; }
    public decimal Cost { get; set; }
    public List<BookModel> Books { get; set; } = new();
}
