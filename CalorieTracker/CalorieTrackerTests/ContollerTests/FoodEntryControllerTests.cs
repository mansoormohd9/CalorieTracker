﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalorieTrackerTests.ContollerTests
{
    public class FoodEntryControllerTests
    {
        private HttpClient _client;
        private ControllerTestBase _factory;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _factory = new ControllerTestBase();
        }


        [TestCase("/FoodEntry")]
        [TestCase("/FoodEntry/Index")]
        [TestCase("/FoodEntry/Details")]
        [TestCase("/FoodEntry/Create")]
        [TestCase("/FoodEntry/Edit")]
        [TestCase("/FoodEntry/DeleteFoodEntry")]
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

        [TestCase("/FoodEntry/DeleteFoodEntryY")]
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
