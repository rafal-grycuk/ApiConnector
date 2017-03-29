using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ApiConnector.Core.HttpRest.Models;
using ApiConnector.Core.HttpRest.Utilities;

namespace ApiConnector.Core.HttpRest
{

    public class ApiConnector : IApiConnector
    {
        public async Task<ApiResponseMessage<T>> RequestAsync<T>(string url, HttpMethod method, object dataObject = null, ContentResponseType? contentResponseType = null, AuthenticationType? authenticationType = null, string authToken = null)
        {
            return await Task.Run(() => Request<T>(url, method, dataObject, contentResponseType, authenticationType, authToken));
        }

        public ApiResponseMessage<T> Request<T>(string url, HttpMethod method, object dataObject = null, ContentResponseType? contentResponseType = null, AuthenticationType? authenticationType = null, string authToken = null)
        {
            HttpRequestMessage message = new HttpRequestMessage(method, url);
            var httpClient = new HttpClient();
            if (authToken != null && authenticationType.HasValue)
                httpClient.DefaultRequestHeaders.Add("Authorization", authenticationType.Value + " " + authToken);
            if (dataObject != null)
            {
                if (contentResponseType.HasValue && contentResponseType.Value == ContentResponseType.JsonContent)
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonObject = Newtonsoft.Json.JsonConvert.SerializeObject(dataObject);
                    message.Content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                }
                else if (contentResponseType.HasValue && contentResponseType.Value == ContentResponseType.TextContent)
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));
                    message.Content = new StringContent(dataObject.ToString(), Encoding.UTF8, "application/text");
                }
                else if (contentResponseType.HasValue && contentResponseType.Value == ContentResponseType.XmlContent)
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                    message.Content = new StringContent(dataObject.ToString(), Encoding.UTF8, "application/xml");
                }
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

