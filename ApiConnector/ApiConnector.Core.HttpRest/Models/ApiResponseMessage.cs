using System.Net.Http;

namespace ApiConnector.Core.HttpRest.Models
{
    public class ApiResponseMessage<T>
    {
        public HttpResponseMessage ResponseMessage { get; set; }
        public T ReponseObject { get; set; }
    }
}
