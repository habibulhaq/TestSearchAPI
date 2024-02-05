using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSearchAPI.Interfaces;

namespace TestSearchAPI.Controllers
{
    /// <summary>
    /// Provides functionality of create , read , update and delete the Users.
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        /// <summary>
        /// User controller initialization
        /// </summary>
        public UserController(IUserService userService)
        {
            this._service = userService;
        }

        /// <summary>
        /// Get All the Users
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            var res = await _service.GetUsers();
            return StatusCode(StatusCodes.Status200OK, res);

        }
    }
}
