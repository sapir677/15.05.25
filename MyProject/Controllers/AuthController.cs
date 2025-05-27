using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyProject.Core.entities;
using MyProject.Core.Interface;
using MyProject.Data;
using MyProject.Service;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _dataContext;



        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController( ILogger<AuthController> logger, DataContext dataContext, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
            _dataContext = dataContext;
        }
        [HttpPost("login")]
        public async Task<ActionResult> Registration(string userName, string password)
        {
            User u = await _authService.Registration(userName, password);
            if (u == null)//אם לא קיים כזה לקוח 
            {
                _logger.LogWarning($"wrong password");
                return Unauthorized();
            }
            return Ok(_authService.GenerateToken(u));
        }

        //[Authorize]
        //[HttpGet("RefreshToken")]
        //public IActionResult RefreshToken(User user)//לדעתי מיותר כקונטרולר
        //{
        //    var token = _authService.GenerateToken(user);
        //    return Ok(token);
        //}
    }
}