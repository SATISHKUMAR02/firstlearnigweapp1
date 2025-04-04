using System.Net;

namespace WebApplication1.Models
{
    public class ApiResponse
    {
        public bool Status { get; set; }

        public HttpStatusCode   StatusCode { get; set; }

        public dynamic data { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

    }
}
