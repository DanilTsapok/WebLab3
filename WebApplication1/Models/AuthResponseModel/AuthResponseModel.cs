using System.Net;

namespace WebApplication1.Models.AuthResponseModel
{
    public class AuthResponseModel<T>
    {
        public T Data { get; set; }
        public string AccessToken { get; set; } 
        public HttpStatusCode StatusCode { get; set; }
    }
}
