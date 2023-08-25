using System.Net;

namespace Bookstore.Domain.Responces;

public class ServiceResponse<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public T Data { get; set; }
}
