using System.Net.Http;
using System.Threading.Tasks;
using ApiConnector.Core.HttpRest.Models;

namespace ApiConnector.Core.HttpRest
{
    public interface IApiConnector
    {
        Task<ApiResponseMessage<T>> RequestAsync<T>(string url, HttpMethod method, object dataObject = null, ContentResponseType? contentResponseType = null, AuthenticationType? authenticationType = null, string authToken = null);
        ApiResponseMessage<T> Request<T>(string url, HttpMethod method, object dataObject = null, ContentResponseType? contentResponseType = null, AuthenticationType? authenticationType = null, string authToken = null);
    }
}
