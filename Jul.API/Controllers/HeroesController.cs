using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Jul.API;
using Jul.Repository.Models;
using Jul.Repository.Interfaces;
using Newtonsoft.Json.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Runtime.ConstrainedExecution;

namespace Jul.API.Controllers
{
//    Controller - CRUD(Update arb selv)
//Endpoint
//    [DataAnnotation]
//http statuscodes
//200,400,500
//Json – data object, array og normal , rigtig og forkert.
//1-M LINQ ?
//Hvis der er mere tid kig på angular til i morgen.

    [Route("api/[controller]")]
    [ApiController] //MVC - Modelstate
    public class HeroesController : ControllerBase
    {
        // DI Dependency Injection - Interface => det er jo ikke rigtigt!! NEJ MEN vi skal benytte Unit Test senere

        private readonly IHeroRepository _context;

        public HeroesController(IHeroRepository context)
        {
            _context = context;
        }

        // GET: api/Heroes
        [HttpGet]
        public async Task<ActionResult> GetHero()
        {
            try
            {
                var heroes = await _context.getAll();
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

        // GET: api/Heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> GetHero(int id)
        {
            try
            {
                var hero = await _context.getById(id);
                if (hero == null)
                {
                    return NotFound();
                }
                return Ok(hero);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
                //throw;
            }
           
        }

        //// PUT: api/Heroes/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutHero(int id, Hero hero)
        //{
        //    if (id != hero.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(hero).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!HeroExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Heroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hero>> PostHero(Hero hero)
        {
            try
            {
                if (hero == null) return BadRequest();
                var temp = await _context.create(hero);
                return Created("", temp);
                //return CreatedAtAction("GetHero", new { id = hero.Id }, hero); // 201
            }
            catch (Exception msg)
            {
                return Problem(msg.Message);
            }          
        }

        //// DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero(int id)
        {
            var hero = await _context.delete(id);
            if (hero == false)
            {
                return NotFound();
            }
            return Ok();//man kan evt. returnere objektet, eller id hvis man skal bruge det.
        }

        //private bool HeroExists(int id)
        //{
        //    return _context.Hero.Any(e => e.Id == id);
        //}
    }
}
