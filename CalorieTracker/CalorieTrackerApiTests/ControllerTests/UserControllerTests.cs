using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerApiTests.ControllerTests
{
    public class UserControllerTests
    {
        private HttpClient _client;
        private ControllerTestBase _factory;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _factory = new ControllerTestBase();
        }


        [TestCase("/api/User/")]
        public async Task Get_EndpointsReturnUnauthorized(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Unauthorized);
        }

        [TestCase("/User/DeleteUserY")]
        public async Task Get_EndpointsReturnFailureStatus(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }
    }
}
