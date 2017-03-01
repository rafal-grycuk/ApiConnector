using System.Net.Http;
using System.Threading.Tasks;
using ApiConnector.Net.HttpRest.Models;

namespace ApiConnector.Net.HttpRest
{
    public interface IApiConnector
    {
        Task<ApiResponseMessage<T>> RequestAsync<T>(string url, HttpMethod method, object dataObject = null, ContentType? contentType = null, string authToken = null);
        ApiResponseMessage<T> Request<T>(string url, HttpMethod method, object dataObject = null, ContentType? contentType = null, string authToken = null);
    }
}
