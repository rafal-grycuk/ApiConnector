using System.Net.Http;
using System.Threading.Tasks;
using ApiConnector.Net.HttpRest;
using ApiConnector.Net.HttpRest.Models;
using ApiConnector.Net.Test.Models;
using Xunit;

namespace ApiConnector.Net.Test
{
    public class RestApiConnectorTests
    {
        private readonly IApiConnector _apiConnector;
        private readonly string _url;
        private readonly string _token;
        public RestApiConnectorTests()
        {
            this._apiConnector = new HttpRest.ApiConnector();
            this._token = "";
            this._url = "https://httpbin.org/";
        }

        [Fact]
        public void GetTestAsync()
        {
            var result = this._apiConnector.RequestAsync<ApiInfo>(this._url + "get", HttpMethod.Get, null, ContentType.JsonContent, this._token).Result;
            Assert.True(result.ResponseMessage.StatusCode == System.Net.HttpStatusCode.OK && result.ReponseObject != null);
        }

        [Fact]
        public void GetTest()
        {
            var result = this._apiConnector.Request<ApiInfo>(this._url + "get", HttpMethod.Get, null, ContentType.JsonContent, this._token);
            Assert.True(result.ResponseMessage.StatusCode == System.Net.HttpStatusCode.OK && result.ReponseObject != null);
        }


    }
}
