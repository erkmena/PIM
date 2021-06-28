using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIM.Model;
using PIM.Model.Responses;
using PIM.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIM.Controllers
{
    [Route("api/brands/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("user/{userId}/products")]
        [ProducesResponseType(typeof(GetBrandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser([FromRoute] int userId)
        {
            var response = await _userService.GetUserAsync(userId);

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }
            if (response.Data == null)
            {
                return NotFound();
            }
            return Ok(response.Data);
        }
    }
}
