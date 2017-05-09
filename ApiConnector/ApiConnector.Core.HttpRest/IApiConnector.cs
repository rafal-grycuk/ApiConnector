using System.Net.Http;
using System.Threading.Tasks;
using ApiConnector.Core.HttpRest.Models;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace ApiConnector.Core.HttpRest
{
    public interface IApiConnector
    {
        Task<ApiResponseMessage<T>> RequestAsync<T>(string url, HttpMethod method, StringContent content = null, ContentResponseType? contentResponseType = null, IDictionary<string, string> defaultRequestHeaders = null, IList<MediaTypeWithQualityHeaderValue> acceptRequestHeaders = null);
        ApiResponseMessage<T> Request<T>(string url, HttpMethod method, StringContent content = null, ContentResponseType? contentResponseType = null, IDictionary<string, string> defaultRequestHeaders = null, IList<MediaTypeWithQualityHeaderValue> acceptRequestHeaders = null);
    }
}
