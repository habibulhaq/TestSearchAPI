using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSearchAPI.Contracts;
using TestSearchAPI.Interfaces;

namespace TestSearchAPI.Controllers
{
    /// <summary>
    /// Provides functionality of token generation
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IUserService _service;
        private readonly IJwtService _jwtService;
        /// <summary>
        /// Token controller initialization
        /// </summary>
        public TokenController(IUserService service, IJwtService jwtService)
        {
            this._service = service;
            this._jwtService = jwtService;
        }

        /// <summary>
        /// Get token against the username and password 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetToken([FromBody] TokenDTO user)
        {
            IActionResult response = Unauthorized();

            var dbUser = await _service.GetByEmailAndPassword(user.Email, user.Password);
            if (dbUser != null)
            {
                if (dbUser.HasError == true)
                {
                    return Unauthorized(new { message = dbUser.Message });
                }

                UserDTOExtension usr = dbUser.Data;
                if (usr == null)
                {
                    return response;
                }

                var token = _jwtService.GenerateSecurityToken(usr);
                return Ok(new { token = token, dbUser.Message });
            }
            else
            {
                return response;
            }
        }
    }
}
