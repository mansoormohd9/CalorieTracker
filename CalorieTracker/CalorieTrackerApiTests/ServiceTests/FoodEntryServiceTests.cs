using CalorieTrackerApi.Models;
using CalorieTrackerApi.Repositories.Interfaces;
using CalorieTrackerApi.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTrackerApiTests.ServiceTests
{
    [TestFixture]
    public class FoodEntryServiceTests: ApiTestBase
    {
        public Mock<IFoodEntryRepo> mockFoodEntryRepo = new Mock<IFoodEntryRepo>();
        public Mock<IUserRepo> mockUserRepo = new Mock<IUserRepo>();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {

        }

        [Test]
        public void TestCreateFoodEntryFail()
        {
            //Arrange
            mockFoodEntryRepo.Setup(x => x.CreateFoodEntry(It.IsAny<string>(), It.IsAny<FoodEntry>())).Returns((false, "Test"));
            var foodEntryService = new FoodEntryService(mockFoodEntryRepo.Object, mockUserRepo.Object, _mapper);

            //Invoke
            var userServiceResult = foodEntryService.CreateFoodEntry("Test", new CalorieTrackerApi.Dtos.CreateFoodEntryDto { Name = "Test" });

            //Assert
            Assert.IsFalse(userServiceResult.Item1);
            Assert.NotNull(userServiceResult.Item2);
        }

        [Test]
        public void TestCreateFoodEntryPass()
        {
            //Arrange
            mockFoodEntryRepo.Setup(x => x.CreateFoodEntry(It.IsAny<string>(), It.IsAny<FoodEntry>())).Returns((true, "Test"));
            var foodEntryService = new FoodEntryService(mockFoodEntryRepo.Object, mockUserRepo.Object, _mapper);

            //Invoke
            var userServiceResult = foodEntryService.CreateFoodEntry("Test", new CalorieTrackerApi.Dtos.CreateFoodEntryDto { Name = "Test" });

            //Assert
            Assert.IsTrue(userServiceResult.Item1);
            Assert.NotNull(userServiceResult.Item2);
        }
    }
}
