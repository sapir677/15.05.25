using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IHourService _hourService;
        private readonly IMapper _mapper;
 
        public HourController(IHourService dataHour,IMapper mapper)
        {
            _hourService = dataHour;
            _mapper = mapper;

        }
        // GET: api/<HourController>
        [HttpGet]
        public async Task<ActionResult<List<HourDTO>>> Get()
        {
            var hours = await _hourService.GetAsync();
            return Ok(_mapper.Map<List<HourDTO>>(hours));
        }


        // GET api/<HourController>/5//לכאורה אין ענין לקבל לפי מזהה עדיף לפי תאריך
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Hour>>> Get(string id)
        {
            List<Hour> tmp = await _hourService.GetByUserId(id);
            if (tmp != null)
                return Ok(tmp);
            return NotFound();
        }

        // GET api/<HourController>/5//נסיון לעשות לפי תאריך 
        [HttpGet("date/{date:datetime}")] // לדוגמה: תאריך בפורמט DateTime
        public async Task<ActionResult<List<Hour>>> Get(DateTime date)
        {
            List<Hour> tmp = await _hourService.GetByDate(date);
            if (tmp != null)
                return Ok(tmp);
            return NotFound();
        }

        // POST api/<HourController>
        [Authorize(Policy = "EmpOnly")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Hour hour)//add
        {
           await _hourService.PostAsync(hour);
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
        // DELETE api/<HourController>/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(string id)//אולי עדיף לקבל id מסוים ולמחוק אותו 
        //{
        //    if (await _dataHour.DeleteAsync(id))
        //        return Ok();
        //    return NotFound();
        //}
    }
}
