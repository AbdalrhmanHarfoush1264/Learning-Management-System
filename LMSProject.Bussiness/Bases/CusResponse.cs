using System.Net;

namespace LMSProject.Bussiness.Bases
{
    public class CusResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int DataCount { get; set; }
        public HttpStatusCode? StatusCode { get; set; }

        public CusResponse()
        {

        }
    }
}
