using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerTests.ContollerTests
{
    [TestFixture]
    public class UserControllerTests: ControllerTestBase<CalorieTracker.Startup>
    {
        private readonly HttpClient _client;
        private readonly ControllerTestBase<CalorieTracker.Startup> _factory;

        public UserControllerTests(
            ControllerTestBase<CalorieTracker.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [TestCase("/User")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.AreEqual("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
