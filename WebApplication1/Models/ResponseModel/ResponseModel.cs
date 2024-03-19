using System.Net;

namespace WebApplication1.Models.ResponseModel
{
    public class ResponseModel<T>
    {
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; set; }

    }
}
