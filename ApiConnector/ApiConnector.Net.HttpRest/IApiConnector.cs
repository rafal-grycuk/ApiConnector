using System.Net.Http;
using System.Threading.Tasks;

namespace ApiConnector.Net.HttpRest
{
    public interface IApiConnector
    {
        Task<T> RequestAsync<T>(string url, HttpMethod method, object dataObject = null, ContentType? contentType = null, string authToken = null);
        T Request<T>(string url, HttpMethod method, object dataObject = null, ContentType? contentType = null, string authToken = null);
    }
}
