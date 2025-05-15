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
        private readonly IUserService _dataUser;
        private readonly IConfiguration _configuration;
        
        //אפשר לעשות כתיבה ללוג ע"י configuration
        public UserController(IUserService data,IConfiguration configuration)
        {
            _dataUser = data;
            _configuration= configuration;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            //Console.WriteLine(_configuration["ApplicationName"]);
            var res = await _dataUser.GetAsync();
            if (res == null)
                return NotFound("user not found");
            return Ok(res);

        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            User user = await _dataUser.GetAsync(id);
            if (user == null)
                return NotFound("user not found");
            return Ok(user);
        }

        // POST api/<UserController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            await _dataUser.PostAsync(user);
            return Ok();
        }

        // PUT api/<UserController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(string id, [FromBody] User user)
        {
            var u = await _dataUser.PutAsync(id, user);
            if (u == null)
                return BadRequest("error");
            return Ok(u);

        }

        // DELETE api/<UserController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
           bool tmp= await _dataUser.DeleteAsync(id);
            if(tmp)
                return Ok();
            return NotFound();
        }
    }
}
