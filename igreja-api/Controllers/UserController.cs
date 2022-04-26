using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nrmcontrolextension.Filters;
using nrmcontrolextension.IServices;
using nrmcontrolextension.Models;

namespace nrmcontrolapi.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _UserService;

        public UserController(IUserService userService)
        {
            this._UserService = userService;
        }

        [HttpGet()]
        [Authorize()]
        public async Task<IActionResult> Get()
        {
            UserFilter userFilter = new();
            try
            {
                return Ok(await _UserService.GetUsers(userFilter));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        [Authorize()]
        public async Task<IActionResult> Get(string userId)
        {
            UserFilter userFilter = new();
            if (!string.IsNullOrEmpty(userId))
            {
                userFilter.UserId = userId;
            }
            try
            {
                return Ok(await _UserService.GetUsers(userFilter));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                return Ok(await _UserService.CreateUser(user));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut()]
        [Authorize()]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            try
            {
                return Ok(await _UserService.UpdateUser(user));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost()]
        [Route("login")]
        public async Task<IActionResult> ValidateLogin([FromBody] User user)
        {
            try
            {
                return Ok(await _UserService.ValidateLogin(user));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Detail = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpDelete("{userId}")]
        [Authorize()]
        public async Task<IActionResult> Delete(string userId)
        {
            User user = new(false, userId, string.Empty);

            try
            {
                await _UserService.DeleteUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
