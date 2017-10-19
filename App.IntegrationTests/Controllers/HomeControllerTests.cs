using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace App.IntegrationTests.Controllers
{
    public class HomeControllerTests
    {
        private readonly HttpClient _httpClient;

        public HomeControllerTests()
        {
            // Arrange
            var webHostBuilder = new WebHostBuilder()
                .UseContentRoot(@"D:\LibDev\GIT\DotNetCore\ASP\IntegrationTestsWithTestServer\App.Web.Mvc")
                .UseStartup<App.Web.Mvc.Startup>();

            var _testServer = new TestServer(webHostBuilder);

            _httpClient = _testServer.CreateClient();
        }

        [Fact]
        public async Task GivenAboutAction_WhenTextIsDisplayed_ThenViewDataTextShouldBeExpected()
        {
            // Act
            var response = await _httpClient.GetAsync("/Home/About");

            // Assert
            response.EnsureSuccessStatusCode();

            var responseHtml = await response.Content.ReadAsStringAsync();

            Assert.Contains("Your application description page.", responseHtml);
        }
    }
}