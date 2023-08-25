namespace Bookstore.Domain.Models;

public class BookModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Released { get; set; }
    public decimal Price { get; set; }
}
