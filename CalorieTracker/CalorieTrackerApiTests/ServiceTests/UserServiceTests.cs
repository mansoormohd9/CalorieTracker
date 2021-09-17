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
    public class UserServiceTests: ApiTestBase
    {
        public Mock<IUserRepo> mockUserRepo = new Mock<IUserRepo>();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            
        }

        [Test]
        public void TestCrateUserFail()
        {
            //Arrange
            mockUserRepo.Setup(x => x.GetUser(It.IsAny<string>())).Returns(new User { });
            var userService = new UserService(mockUserRepo.Object, _mapper);

            //Invoke
            var userServiceResult = userService.CreateUser(new CalorieTrackerApi.Dtos.UserDto { UserName = "Test" });

            //Assert
            Assert.IsFalse(userServiceResult.Item1);
            Assert.NotNull(userServiceResult.Item2);
        }

        [Test]
        public void TestCrateUserPass()
        {
            //Arrange
            User user = null;
            mockUserRepo.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            mockUserRepo.Setup(x => x.CreateUser(It.IsAny<User>())).Returns((true, "Test"));
            var userService = new UserService(mockUserRepo.Object, _mapper);

            //Invoke
            var userServiceResult = userService.CreateUser(new CalorieTrackerApi.Dtos.UserDto { UserName = "Test" });

            //Assert
            Assert.IsTrue(userServiceResult.Item1);
            Assert.NotNull(userServiceResult.Item2);
        }
    }
}
