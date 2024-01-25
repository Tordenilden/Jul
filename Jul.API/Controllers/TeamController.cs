using Jul.Repository.Interfaces;
using Jul.Repository.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jul.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository context;

        public TeamController(ITeamRepository c)
        {
            context = c;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeam()
        {
            try
            {
                var heroes = await context.getAll();
                if (heroes == null)
                {
                    return Problem("You got a cookie"); // NotFound()
                }
                else if (heroes.Count == 0)
                {
                    return NoContent();
                }
                return Ok(heroes);
            }
            catch (Exception msg)
            {
                // ILogger =>
                return Problem(msg.Message); // we can make our own
            }
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TeamController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TeamController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
