using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestSearchAPI.Contracts;
using TestSearchAPI.Interfaces;

namespace TestSearchAPI.Controllers
{
    /// <summary>
    /// Provides functionality of searching of Items.
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class ItemController : Controller
    {
        private readonly IItemService _service;
        /// <summary>
        /// Item controller initialization
        /// </summary>
        public ItemController(IItemService itemService)
        {
            this._service = itemService;
        }

        /// <summary>
        /// Filter the items record based on the search criteria
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<IActionResult> SearchItems([FromQuery] SearchCriteriaDTO payload)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.UserData));
            var res = await _service.SearchItems(payload, userId);
            return StatusCode(StatusCodes.Status200OK, res);

        }

        /// <summary>
        /// Get user's search history
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<IActionResult> GetSearchHistoryAsync()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.UserData));
            var res = await _service.GetSearchHistoryAsync(userId);
            return StatusCode(StatusCodes.Status200OK, res);

        }
    }
}
