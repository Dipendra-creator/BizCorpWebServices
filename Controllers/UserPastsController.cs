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
    public class UserPastsController : ControllerBase
    {
        private readonly BizCorpContext _context;

        public UserPastsController(BizCorpContext context)
        {
            _context = context;
        }

        // GET: api/UserPasts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPast>>> GetUserPasts()
        {
          if (_context.UserPasts == null)
          {
              return NotFound();
          }
            return await _context.UserPasts.ToListAsync();
        }

        // GET: api/UserPasts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPast>> GetUserPast(int id)
        {
          if (_context.UserPasts == null)
          {
              return NotFound();
          }
            var userPast = await _context.UserPasts.FindAsync(id);

            if (userPast == null)
            {
                return NotFound();
            }

            return userPast;
        }

        // PUT: api/UserPasts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPast(int id, UserPast userPast)
        {
            if (id != userPast.UserPastId)
            {
                return BadRequest();
            }

            _context.Entry(userPast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPastExists(id))
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

        // POST: api/UserPasts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserPast>> PostUserPast(UserPast userPast)
        {
          if (_context.UserPasts == null)
          {
              return Problem("Entity set 'BizCorpContext.UserPasts'  is null.");
          }
            _context.UserPasts.Add(userPast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPast", new { id = userPast.UserPastId }, userPast);
        }

        // DELETE: api/UserPasts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPast(int id)
        {
            if (_context.UserPasts == null)
            {
                return NotFound();
            }
            var userPast = await _context.UserPasts.FindAsync(id);
            if (userPast == null)
            {
                return NotFound();
            }

            _context.UserPasts.Remove(userPast);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserPastExists(int id)
        {
            return (_context.UserPasts?.Any(e => e.UserPastId == id)).GetValueOrDefault();
        }
    }
}
