using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyProject.Core.entities;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;


        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("login")]
        public ActionResult Registration([FromBody] LoginRequest model)
        {
            User user = null;

            if (model.Id?.ToUpper() == "ADMIN" && model.Password == "123456")
            {
                user = new User
                {
                    Id = "ADMIN",
                    Name = "Admin",
                    Usertype = USERTYPE.ADMIN
                };
                return Ok(GenerateToken(user));
            }
            else if (user == null || !VerifyPassword(model.Password, user.Password,model))
            {
                _logger.LogWarning($"wrong password");
                return Unauthorized();
            }
            else
            {
                //אם קיים 
                //Generate token
                var token = GenerateToken(user);
                return Ok(token);
            }
        }
        private bool VerifyPassword(string id,string password,LoginRequest model)
        {   
            return model.Id==id&&model.Password==password;
        }

        [Authorize]
        [HttpGet("RefreshToken")]
        public IActionResult RefreshToken(User user)
        {
            var token = GenerateToken(user);
            return Ok(token);
        }
        private string GenerateToken(User user)
        {
            var claims = new[]
            {
               // new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Role, user.Usertype.ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
    public class LoginRequest
    {
        public string Id { get; set; }
        public string Password { get; set; }
    }
}
