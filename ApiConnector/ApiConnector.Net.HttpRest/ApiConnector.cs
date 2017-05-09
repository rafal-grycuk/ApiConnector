using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ApiConnector.Net.HttpRest.Models;
using ApiConnector.Net.HttpRest.Utilities;
using System.Linq;
using System.Collections.Generic;

namespace ApiConnector.Net.HttpRest
{
    public class ApiConnector : IApiConnector
    {
        public async Task<ApiResponseMessage<T>> RequestAsync<T>(string url, HttpMethod method, StringContent content = null, ContentResponseType? contentResponseType = null, IDictionary<string, string> defaultRequestHeaders = null, IList<MediaTypeWithQualityHeaderValue> acceptRequestHeaders = null)
        {
            return await Task.Run(() => Request<T>(url, method, content, contentResponseType, defaultRequestHeaders, acceptRequestHeaders));
        }

        public ApiResponseMessage<T> Request<T>(string url, HttpMethod method, StringContent content = null, ContentResponseType? contentResponseType = null, IDictionary<string, string> defaultRequestHeaders = null, IList<MediaTypeWithQualityHeaderValue> acceptRequestHeaders = null)
        {
            HttpRequestMessage message = new HttpRequestMessage(method, url);
            var httpClient = new HttpClient();
            foreach (var header in defaultRequestHeaders ?? Enumerable.Empty<KeyValuePair<string, string>>())
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            foreach (var acceptHeader in acceptRequestHeaders ?? Enumerable.Empty<MediaTypeWithQualityHeaderValue>())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(acceptHeader);
            }
            if (content != null)
            {
                message.Content = content;
            }
            var responseMessage = httpClient.SendAsync(message).Result;
            ApiResponseMessage<T> apiResponseMessage = new ApiResponseMessage<T>()
            {
                ResponseMessage = responseMessage
            };
            if (responseMessage.IsSuccessStatusCode)
            {
                if (contentResponseType.HasValue && contentResponseType.Value == ContentResponseType.JsonContent)
                {
                    string jsonString = responseMessage.Content.ReadAsStringAsync().Result;
                    var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
                    apiResponseMessage.ReponseObject = responseObject;
                }
                else if (contentResponseType.HasValue && contentResponseType.Value == ContentResponseType.TextContent)
                {
                    string textString = responseMessage.Content.ReadAsStringAsync().Result;
                    apiResponseMessage.ReponseObject = (dynamic)textString;
                }
                else if (contentResponseType.HasValue && contentResponseType.Value == ContentResponseType.XmlContent)
                {
                    string xmlString = responseMessage.Content.ReadAsStringAsync().Result;
                    apiResponseMessage.ReponseObject = xmlString.XmlDeserialize<T>();
                }
            }
            return apiResponseMessage;
        }
    }
}


