using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using igrejaextensions.IServices;
using igrejaextensions.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace igreja_api.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private const int TIME_TO_EXPIRE_MIN = 240;

        public LoginController(IConfiguration config, IUserService userService)
        {
            _configuration = config;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User _userData)
        {
            try
            {
                if (_userData != null && _userData.Password != null)
                {
                    if (!await _userService.ValidateLogin(_userData))
                    {
                        return Unauthorized(new { Detail = "Informe usuário e senha" });
                    }
                    var user = _userData;

                    DateTime dateExpire = DateTime.UtcNow.AddMinutes(TIME_TO_EXPIRE_MIN);
                    DateTime dataAgora = DateTime.UtcNow;

                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, dataAgora.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: dateExpire,
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return Unauthorized(new { Detail = "Informe usuário e senha" });
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
