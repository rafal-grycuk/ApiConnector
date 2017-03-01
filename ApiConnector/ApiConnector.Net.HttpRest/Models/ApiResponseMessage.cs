using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiConnector.Net.HttpRest.Models
{
    public class ApiResponseMessage<T>
    {
        public HttpResponseMessage ResponseMessage { get; set; }
        public T ReponseObject { get; set; }
    }
}
