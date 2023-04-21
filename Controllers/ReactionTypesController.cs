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
    public class ReactionTypesController : ControllerBase
    {
        private readonly BizCorpContext _context;

        public ReactionTypesController(BizCorpContext context)
        {
            _context = context;
        }

        // GET: api/ReactionTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReactionType>>> GetReactionTypes()
        {
          if (_context.ReactionTypes == null)
          {
              return NotFound();
          }
            return await _context.ReactionTypes.ToListAsync();
        }

        // GET: api/ReactionTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReactionType>> GetReactionType(int id)
        {
          if (_context.ReactionTypes == null)
          {
              return NotFound();
          }
            var reactionType = await _context.ReactionTypes.FindAsync(id);

            if (reactionType == null)
            {
                return NotFound();
            }

            return reactionType;
        }

        // PUT: api/ReactionTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReactionType(int id, ReactionType reactionType)
        {
            if (id != reactionType.ReactionTypeId)
            {
                return BadRequest();
            }

            _context.Entry(reactionType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReactionTypeExists(id))
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

        // POST: api/ReactionTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReactionType>> PostReactionType(ReactionType reactionType)
        {
          if (_context.ReactionTypes == null)
          {
              return Problem("Entity set 'BizCorpContext.ReactionTypes'  is null.");
          }
            _context.ReactionTypes.Add(reactionType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReactionType", new { id = reactionType.ReactionTypeId }, reactionType);
        }

        // DELETE: api/ReactionTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReactionType(int id)
        {
            if (_context.ReactionTypes == null)
            {
                return NotFound();
            }
            var reactionType = await _context.ReactionTypes.FindAsync(id);
            if (reactionType == null)
            {
                return NotFound();
            }

            _context.ReactionTypes.Remove(reactionType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReactionTypeExists(int id)
        {
            return (_context.ReactionTypes?.Any(e => e.ReactionTypeId == id)).GetValueOrDefault();
        }
    }
}
