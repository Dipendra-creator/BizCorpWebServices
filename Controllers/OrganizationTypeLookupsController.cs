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
    public class OrganizationTypeLookupsController : ControllerBase
    {
        private readonly BizCorpContext _context;

        public OrganizationTypeLookupsController(BizCorpContext context)
        {
            _context = context;
        }

        // GET: api/OrganizationTypeLookups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationTypeLookup>>> GetOrganizationTypeLookups()
        {
          if (_context.OrganizationTypeLookups == null)
          {
              return NotFound();
          }
            return await _context.OrganizationTypeLookups.ToListAsync();
        }

        // GET: api/OrganizationTypeLookups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationTypeLookup>> GetOrganizationTypeLookup(int id)
        {
          if (_context.OrganizationTypeLookups == null)
          {
              return NotFound();
          }
            var organizationTypeLookup = await _context.OrganizationTypeLookups.FindAsync(id);

            if (organizationTypeLookup == null)
            {
                return NotFound();
            }

            return organizationTypeLookup;
        }

        // PUT: api/OrganizationTypeLookups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizationTypeLookup(int id, OrganizationTypeLookup organizationTypeLookup)
        {
            if (id != organizationTypeLookup.OrganizationTypeLookupId)
            {
                return BadRequest();
            }

            _context.Entry(organizationTypeLookup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationTypeLookupExists(id))
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

        // POST: api/OrganizationTypeLookups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrganizationTypeLookup>> PostOrganizationTypeLookup(OrganizationTypeLookup organizationTypeLookup)
        {
          if (_context.OrganizationTypeLookups == null)
          {
              return Problem("Entity set 'BizCorpContext.OrganizationTypeLookups'  is null.");
          }
            _context.OrganizationTypeLookups.Add(organizationTypeLookup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganizationTypeLookup", new { id = organizationTypeLookup.OrganizationTypeLookupId }, organizationTypeLookup);
        }

        // DELETE: api/OrganizationTypeLookups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationTypeLookup(int id)
        {
            if (_context.OrganizationTypeLookups == null)
            {
                return NotFound();
            }
            var organizationTypeLookup = await _context.OrganizationTypeLookups.FindAsync(id);
            if (organizationTypeLookup == null)
            {
                return NotFound();
            }

            _context.OrganizationTypeLookups.Remove(organizationTypeLookup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizationTypeLookupExists(int id)
        {
            return (_context.OrganizationTypeLookups?.Any(e => e.OrganizationTypeLookupId == id)).GetValueOrDefault();
        }
    }
}
