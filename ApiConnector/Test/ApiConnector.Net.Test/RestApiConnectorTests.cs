using System.Net.Http;
using System.Threading.Tasks;
using ApiConnector.Net.HttpRest;
using ApiConnector.Net.HttpRest.Models;
using ApiConnector.Net.Test.Models;
using Xunit;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace ApiConnector.Net.Test
{
    public class RestApiConnectorTests
    {
        private readonly IApiConnector _apiConnector;
        public RestApiConnectorTests()
        {
            this._apiConnector = new HttpRest.ApiConnector();
        }

        [Fact]
        public void GetTestAsync()
        {
            var acceptHeaders = new List<MediaTypeWithQualityHeaderValue>()
            {
                new MediaTypeWithQualityHeaderValue("application/json")
            };
            var result = this._apiConnector.RequestAsync<ApiInfo>("https://httpbin.org/get", HttpMethod.Get, null, ContentResponseType.JsonContent).Result;
            Assert.True(result != null && result.ResponseMessage.IsSuccessStatusCode && result.ReponseObject != null);
        }

        [Fact]
        public void GetTest()
        {
            var acceptHeaders = new List<MediaTypeWithQualityHeaderValue>()
            {
                new MediaTypeWithQualityHeaderValue("application/json")
            };
            var result = this._apiConnector.Request<ApiInfo>("https://httpbin.org/get", HttpMethod.Get, null, ContentResponseType.JsonContent);
            Assert.True(result != null && result.ResponseMessage.IsSuccessStatusCode && result.ReponseObject != null);
        }

        [Fact]
        public void GetTextTest()
        {
            var acceptHeaders = new List<MediaTypeWithQualityHeaderValue>()
            {
                new MediaTypeWithQualityHeaderValue("application/text")
            };
            var result = this._apiConnector.Request<string>("http://rrag.github.io/react-stockcharts/data/MSFT.tsv", HttpMethod.Get, null, ContentResponseType.TextContent);
            Assert.True(result != null && result.ResponseMessage.IsSuccessStatusCode && result.ReponseObject != null);
        }

        [Fact]
        public void GetXmlTest()
        {
            var acceptHeaders = new List<MediaTypeWithQualityHeaderValue>()
            {
                new MediaTypeWithQualityHeaderValue("application/xml")
            };
            var result = this._apiConnector.Request<Customer>("http://parabank.parasoft.com/parabank/services/bank/customers/12212/", HttpMethod.Get, null, ContentResponseType.XmlContent, null, acceptHeaders);
            Assert.True(result != null && result.ResponseMessage.IsSuccessStatusCode && result.ReponseObject != null);
        }
    }
}

