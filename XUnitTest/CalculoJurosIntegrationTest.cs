using Service;
using Api;
using System;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Api.Controllers;
using Config;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Testing;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace XUnitTest
{
    public class CalculoJurosIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public CalculoJurosIntegrationTest(WebApplicationFactory<Startup> factory)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(configPath);
                });

            });
           
        }

        /// <summary>
        /// Teste para verificar se o retorno igual a 200 OK 
        /// Obs.: Api 1 - Ecommerce.Taxas deve estar Up and runnning.
        /// </summary>
        /// <param name="method">Get</param>
        /// <returns>200 OK</returns>
        [Theory]
        [InlineData("GET")]
        public async Task DeveRetornarSucessoHttpStatusCodeOK(string method)
        {
            //Arrange
            var client = _factory.CreateClient();
            var url = "https://localhost:44304/CalculoJuros/calculaJuros?valorInicial=567&tempo=6";
            var request = new HttpRequestMessage(new HttpMethod(method), url);

            //Act
            var response = await client.GetAsync(url);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
