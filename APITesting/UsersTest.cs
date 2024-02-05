using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestSearchAPI.Contracts;
using TestSearchAPI.Controllers;
using TestSearchAPI.Interfaces;
using TestSearchAPI.Services;
using TestSearchAPI.Services.DummyRepository;

namespace APITesting
{
    [TestClass]
    public class UsersTest
    {

        private UserController _userController;
        private IUserService _mockUserService;

        [TestInitialize]
        public void Intialize()
        {
            _mockUserService = new DummyUserService();
            _userController = new UserController(_mockUserService);
        }

        [TestMethod]
        public void GetUserTest()
        {

            var result = _userController.GetUsers().Result;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.ObjectResult));

            var objResult = result as Microsoft.AspNetCore.Mvc.ObjectResult;
            Assert.IsNotNull(objResult);
            Assert.AreEqual(StatusCodes.Status200OK, objResult.StatusCode);

            ApiResponse<IEnumerable<UserDTO>> apiResponse = objResult.Value as ApiResponse<IEnumerable<UserDTO>>;

            Assert.IsTrue(apiResponse.HasError == false);
            Assert.IsTrue(apiResponse.Count > 0);
        }

    }
}
