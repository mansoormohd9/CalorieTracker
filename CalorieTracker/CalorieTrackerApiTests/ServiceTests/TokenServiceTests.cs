using CalorieTrackerApi.Dtos;
using CalorieTrackerApi.Models;
using CalorieTrackerApi.Repositories.Interfaces;
using CalorieTrackerApi.Services;
using CalorieTrackerApi.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTrackerApiTests.ServiceTests
{
    [TestFixture]
    public class TokenServiceTests: ApiTestBase
    {
        public Mock<ITokenRepo> mockTokenRepo = new Mock<ITokenRepo>();
        public Mock<IUserService> mockUserService = new Mock<IUserService>();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {

        }

        [Test]
        public void TestCreateUserTokenFail()
        {
            //Arrange
            mockUserService.Setup(x => x.GetUser(It.IsAny<string>())).Returns((false, null));
            var tokenService = new TokenService(mockTokenRepo.Object, _mapper, mockUserService.Object);

            //Invoke
            var userServiceResult = tokenService.CreateUserToken(new CalorieTrackerApi.Dtos.CreateTokenDto { UserName = "Test" });

            //Assert
            Assert.IsFalse(userServiceResult.Item1);
            Assert.NotNull(userServiceResult.Item2);
        }

        [Test]
        public void TestGetUserTokenFail()
        {
            //Arrange
            UserToken userToken = null;
            mockTokenRepo.Setup(x => x.GetUserToken(It.IsAny<Guid>())).Returns(userToken);
            var tokenService = new TokenService(mockTokenRepo.Object, _mapper, mockUserService.Object);

            //Invoke
            var userServiceResult = tokenService.GetUserToken(Guid.NewGuid());

            //Assert
            Assert.IsFalse(userServiceResult.Item1);
            Assert.IsNull(userServiceResult.Item2);
        }
    }
}
