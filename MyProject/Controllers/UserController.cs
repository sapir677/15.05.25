using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Core.DTOs;
using MyProject.Core.entities;
using MyProject.Core.Interface;
using MyProject.Service;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        
        //אפשר לעשות כתיבה ללוג ע"י configuration
        public UserController(IUserService data,IConfiguration configuration)
        {
            _userService = data;
            _configuration= configuration;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
           // Console.WriteLine(_configuration["ApplicationName"]);
            var res = await _userService.GetAsync();
            if (res == null)
                return NotFound("user not found");
            return Ok(res);

        }

        // GET api/<UserController>/5
        [Authorize(Policy = "EmpOnly")]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            User user = await _userService.GetAsync(id);
            if (user == null)
                return NotFound("user not found");
            return Ok(user);
        }

        // POST api/<UserController>
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            if (user == null)
                return BadRequest("can not get a uninitial value");
            User u= await _userService.PostAsync(user);
            if (u == null)
                return Conflict("user already exists id: " + user.Id);
            return Ok(user);
        }

        // PUT api/<UserController>/5
        [Authorize(Policy= "UserOnly")]//כדי שמשתמש יוכל לעדכן את עצמו כניסה לפי מס זהות
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(string id, [FromBody] User user)
        {
            var u = await _userService.PutAsync(id, user);
            if (u == null)
                return BadRequest("error");
            return Ok(u);

        }

        // DELETE api/<UserController>/5
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
           bool tmp= await _userService.DeleteAsync(id);
            if(tmp)
                return Ok();
            return NotFound();
        }
    }
}
