using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyProject.Core.DTOs;
using MyProject.Core.entities;
using MyProject.Core.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HourController : ControllerBase
    {
        private readonly IHourService _dataHour;
        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;
 
        public HourController(IHourService dataHour, IConfiguration configuration)
        {
            _dataHour = dataHour;
            _configuration = configuration;

        }
        // GET: api/<HourController>
        [HttpGet]
        public async Task<ActionResult<List<HourDTO>>> Get()
        {
            var hours = await _dataHour.GetAsync();
            return Ok(_mapper.Map<List<HourDTO>>(hours));
        }


        // GET api/<HourController>/5//לכאורה אין ענין לקבל לפי מזהה עדיף לפי תאריך
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Hour>>> Get(string id)
        {
            List<Hour> tmp = await _dataHour.GetByUserId(id);
            if (tmp != null)
                return Ok(tmp);
            return NotFound();
        }

        // GET api/<HourController>/5//נסיון לעשות לפי תאריך 
        [HttpGet("{date}")]
        public async Task<ActionResult<List<Hour>>> Get(DateTime id)
        {
            List<Hour> tmp = await _dataHour.GetByUserId(id);
            if (tmp != null)
                return Ok(tmp);
            return NotFound();
        }

        // POST api/<HourController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Hour hour)//add
        {
           await _dataHour.PostAsync(hour);
            return Ok();
        }

        //// PUT api/<HourController>/5
        //[HttpPut("{id}")]
        //public async Task<ActionResult<Hour>> Put(string id, [FromBody] Hour hour)//update
        //{
        //    if (hour == null)
        //        return NotFound("not found");
        //  return Ok(await _dataHour.PutAsync(id, hour));
        //}
//??
        //// DELETE api/<HourController>/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(string id)//אולי עדיף לקבל id מסוים ולמחוק אותו 
        //{
        //    if (await _dataHour.DeleteAsync(id))
        //        return Ok();
        //    return NotFound();
        //}
    }
}
