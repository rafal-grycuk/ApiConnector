using System.Net.Http;
using ApiConnector.Core.HttpRest;
using ApiConnector.Core.HttpRest.Models;
using ApiConnector.Core.Test.Models;
using Xunit;

namespace ApiConnector.Core.Test
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
            var result = this._apiConnector.RequestAsync<ApiInfo>(this._url + "get", HttpMethod.Get, null, ContentResponseType.JsonContent, this._token).Result;
        }

        [Fact]
        public void GetTest()
        {
            var result = this._apiConnector.Request<ApiInfo>(this._url + "get", HttpMethod.Get, null, ContentResponseType.JsonContent, this._token);
        }


    }
}
