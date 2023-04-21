using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BizCorp.Data;
using BizCorp.Models;

namespace BizCorp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityLookupsController : ControllerBase
    {
        private readonly BizCorpContext _context;

        public CityLookupsController(BizCorpContext context)
        {
            _context = context;
        }

        // GET: api/CityLookups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityLookup>>> GetCityLookups()
        {
          if (_context.CityLookups == null)
          {
              return NotFound();
          }
            return await _context.CityLookups.ToListAsync();
        }

        // GET: api/CityLookups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CityLookup>> GetCityLookup(int id)
        {
          if (_context.CityLookups == null)
          {
              return NotFound();
          }
            var cityLookup = await _context.CityLookups.FindAsync(id);

            if (cityLookup == null)
            {
                return NotFound();
            }

            return cityLookup;
        }

        // PUT: api/CityLookups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCityLookup(int id, CityLookup cityLookup)
        {
            if (id != cityLookup.CityLookupId)
            {
                return BadRequest();
            }

            _context.Entry(cityLookup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityLookupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CityLookups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CityLookup>> PostCityLookup(CityLookup cityLookup)
        {
          if (_context.CityLookups == null)
          {
              return Problem("Entity set 'BizCorpContext.CityLookups'  is null.");
          }
            _context.CityLookups.Add(cityLookup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCityLookup", new { id = cityLookup.CityLookupId }, cityLookup);
        }

        // DELETE: api/CityLookups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCityLookup(int id)
        {
            if (_context.CityLookups == null)
            {
                return NotFound();
            }
            var cityLookup = await _context.CityLookups.FindAsync(id);
            if (cityLookup == null)
            {
                return NotFound();
            }

            _context.CityLookups.Remove(cityLookup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityLookupExists(int id)
        {
            return (_context.CityLookups?.Any(e => e.CityLookupId == id)).GetValueOrDefault();
        }
    }
}
