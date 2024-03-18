using System.Net;

namespace WebApplication1.Models
{
    public class ResponseModel<T>
    {
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<T>? Data { get; set; }
    }
}
