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
        public RestApiConnectorTests()
        {
            this._apiConnector = new HttpRest.ApiConnector();
        }

        [Fact]
        public void GetTestAsync()
        {
            var result = this._apiConnector.RequestAsync<ApiInfo>( "https://httpbin.org/get", HttpMethod.Get, null, ContentResponseType.JsonContent).Result;
        }

        [Fact]
        public void GetTest()
        {
            var result = this._apiConnector.Request<ApiInfo>("https://httpbin.org/get", HttpMethod.Get, null, ContentResponseType.JsonContent);
        }

        [Fact]
        public void GetTextTest()
        {
            var result = this._apiConnector.Request<string>("http://rrag.github.io/react-stockcharts/data/MSFT.tsv", HttpMethod.Get, null, ContentResponseType.TextContent);
        }

        [Fact]
        public void GetXmlTest()
        {
            var result = this._apiConnector.Request<Customer>("http://parabank.parasoft.com/parabank/services/bank/customers/12212/", HttpMethod.Get, null, ContentResponseType.XmlContent);
        }


    }
}
