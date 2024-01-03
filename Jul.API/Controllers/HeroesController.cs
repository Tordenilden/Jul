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

namespace Jul.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<IEnumerable<Hero>>> GetHero()
        {
            return await _context.getAll();
        }

        // GET: api/Heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> GetHero(int id)
        {
            var hero = await _context.getById(id);

            if (hero == null)
            {
                return NotFound();
            }

            return Ok(hero);
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

        //// POST: api/Heroes
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Hero>> PostHero(Hero hero)
        //{
        //    _context.Hero.Add(hero);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetHero", new { id = hero.Id }, hero);
        //}

        //// DELETE: api/Heroes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteHero(int id)
        //{
        //    var hero = await _context.Hero.FindAsync(id);
        //    if (hero == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Hero.Remove(hero);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool HeroExists(int id)
        //{
        //    return _context.Hero.Any(e => e.Id == id);
        //}
    }
}
