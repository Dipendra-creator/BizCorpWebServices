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
    public class CountryLookupsController : ControllerBase
    {
        private readonly BizCorpContext _context;

        public CountryLookupsController(BizCorpContext context)
        {
            _context = context;
        }

        // GET: api/CountryLookups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryLookup>>> GetCountryLookups()
        {
          if (_context.CountryLookups == null)
          {
              return NotFound();
          }
            return await _context.CountryLookups.ToListAsync();
        }

        // GET: api/CountryLookups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryLookup>> GetCountryLookup(int id)
        {
          if (_context.CountryLookups == null)
          {
              return NotFound();
          }
            var countryLookup = await _context.CountryLookups.FindAsync(id);

            if (countryLookup == null)
            {
                return NotFound();
            }

            return countryLookup;
        }

        // PUT: api/CountryLookups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryLookup(int id, CountryLookup countryLookup)
        {
            if (id != countryLookup.CountryLookupId)
            {
                return BadRequest();
            }

            _context.Entry(countryLookup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryLookupExists(id))
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

        // POST: api/CountryLookups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CountryLookup>> PostCountryLookup(CountryLookup countryLookup)
        {
          if (_context.CountryLookups == null)
          {
              return Problem("Entity set 'BizCorpContext.CountryLookups'  is null.");
          }
            _context.CountryLookups.Add(countryLookup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountryLookup", new { id = countryLookup.CountryLookupId }, countryLookup);
        }

        // DELETE: api/CountryLookups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryLookup(int id)
        {
            if (_context.CountryLookups == null)
            {
                return NotFound();
            }
            var countryLookup = await _context.CountryLookups.FindAsync(id);
            if (countryLookup == null)
            {
                return NotFound();
            }

            _context.CountryLookups.Remove(countryLookup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryLookupExists(int id)
        {
            return (_context.CountryLookups?.Any(e => e.CountryLookupId == id)).GetValueOrDefault();
        }
    }
}
