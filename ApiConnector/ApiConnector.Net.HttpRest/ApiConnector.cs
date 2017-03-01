using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ApiConnector.Net.HttpRest.Models;

namespace ApiConnector.Net.HttpRest
{

    public class ApiConnector : IApiConnector
    {
        public async Task<ApiResponseMessage<T>> RequestAsync<T>(string url, HttpMethod method, object dataObject = null, ContentType? contentType = null, string authToken = null)
        {
            return await Task.Run(() => Request<T>(url, method, dataObject, contentType, authToken));
        }

        public ApiResponseMessage<T> Request<T>(string url, HttpMethod method, object dataObject = null, ContentType? contentType = null, string authToken = null)
        {
            HttpRequestMessage message = new HttpRequestMessage(method, url);
            var httpClient = new HttpClient();
            if (authToken != null)
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + authToken);
            if (dataObject != null)
            {
                if (contentType.HasValue && contentType.Value == ContentType.JsonContent)
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonObject = Newtonsoft.Json.JsonConvert.SerializeObject(dataObject);
                    message.Content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                }
                else if (contentType.HasValue && contentType.Value == ContentType.TextContent)
                {
                    httpClient.DefaultRequestHeaders.Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/text"));
                    message.Content = new StringContent(dataObject.ToString(), Encoding.UTF8, "application/text");
                }
                else if (contentType.HasValue && contentType.Value == ContentType.XmlContent)
                {
                    throw new NotImplementedException("xml content type was not implemented yet :)");
                }
            }

            var responseMessage = httpClient.SendAsync(message).Result;
            ApiResponseMessage<T> apiResponseMessage = new ApiResponseMessage<T>()
            {
                ResponseMessage = responseMessage
            };
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonString = responseMessage.Content.ReadAsStringAsync().Result;
                var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
                apiResponseMessage.ReponseObject = responseObject;
            }
            return apiResponseMessage;
        }
    }
}


