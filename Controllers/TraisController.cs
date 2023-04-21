using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BizCorp.Data;
using BizCorp.Models;
using System.Globalization;

namespace BizCorp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraisController : ControllerBase
    {
        private readonly BizCorpContext _context;

        public TraisController(BizCorpContext context)
        {
            _context = context;
        }

        // GET: api/Trais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trai>>> GetTrais()
        {
          if (_context.Trais == null)
          {
              return NotFound();
          }
            return await _context.Trais.ToListAsync();
        }

        // GET: api/Trais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trai>> GetTrai(int id)
        {
            if (_context.Trais == null)
            {
                return NotFound();
            }
            var trai = await _context.Trais.FindAsync(id);

            if (trai == null)
            {
                return NotFound();
            }

            return trai;
        }

        // PUT: api/Trais/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrai(int id, Trai trai)
        {
            if (id != trai.TraiId)
            {
                return BadRequest();
            }

            _context.Entry(trai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraiExists(id))
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

        // POST: api/Trais
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trai>> PostTrai(Trai trai)
        {
            if (_context.Trais == null)
            {
                return Problem("Entity set 'BizCorpContext.Trais'  is null.");
            }
            _context.Trais.Add(trai);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrai", new { id = trai.TraiId }, trai);
        }

        // DELETE: api/Trais/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrai(int id)
        {
            if (_context.Trais == null)
            {
                return NotFound();
            }
            var trai = await _context.Trais.FindAsync(id);
            if (trai == null)
            {
                return NotFound();
            }

            _context.Trais.Remove(trai);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Trais/pnum/8538538532
        [HttpGet("pnum/{pnum}")]
        public async Task<ActionResult<List<Trai>>> GetTraiNum(string pnum)
        {
            if (_context.Trais == null)
            {
                return NotFound();
            }

            var trais = await _context.Trais.Where(t => t.PNumber == pnum).ToListAsync();

            if (trais.Count == 0)
            {
                return NotFound();
            }

            return trais;
        }

        [HttpGet("search/{pnum}/{year}")]
        public async Task<ActionResult<IEnumerable<Trai>>> GetTraisByPNumberAndYear(string pnum, string year)
        {
            var trais = _context.Trais
                .Where(t => t.PNumber == pnum && t.Year == year)
                .ToListAsync();

            if (trais == null)
            {
                return NotFound();
            }

            return await trais;
        }

        [HttpGet("by-month-year/{pnum}/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<Trai>>> GetTraisByMonthYear(string pnum, string month, string year)
        {
            // Convert the input month to the matching format in the database
            string[] monthNames = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            
            int monthNum;
            month = (int.TryParse(month, out monthNum)) ? 
                monthNames[monthNum - 1] : 
                CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month).ToUpper();

            // Get the list of numbers based on the given parameters
            var trais = await _context.Trais.Where(t => t.PNumber == pnum && t.Month == month && t.Year == year).ToListAsync();

            if (trais == null || !trais.Any())
            {
                return NotFound();
            }

            return trais;
        }

        private bool TraiExists(int id)
        {
            return (_context.Trais?.Any(e => e.TraiId == id)).GetValueOrDefault();
        }
    }
}
