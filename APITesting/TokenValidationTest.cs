using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using TestSearchAPI.Contracts;
using TestSearchAPI.Controllers;
using TestSearchAPI.Interfaces;
using TestSearchAPI.Services;
using TestSearchAPI.Services.DummyRepository;

namespace APITesting
{
    [TestClass]
    public class TokenValidationTest
    {

        private TokenController _tokenController;
        private IUserService _mockUserService;
        private IJwtService _mockJwtService;

        [TestInitialize]
        public void Setup()
        {
            _mockUserService = new DummyUserService();
            _mockJwtService = new DummyJwtService();
            _tokenController = new TokenController(_mockUserService, _mockJwtService);
        }

        [TestMethod]
        public void ValidAthenticationTest()
        {
            var userCredentials = new TokenDTO
            {
                Email = "abc@gmail.com",
                Password = "abcd1234"
            };

            var result = _tokenController.GetToken(userCredentials).Result;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void InvalidAuthenticationTest()
        {
            var userCredentials = new TokenDTO
            {
                Email = "invalid@gmail.com",
                Password = "invalidpassword"
            };

            var result = _tokenController.GetToken(userCredentials).Result;


            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));

            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.AreEqual(401, unauthorizedResult.StatusCode);
           
        }


    }
}
