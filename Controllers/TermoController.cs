using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EPBD2.Models;

namespace EPBD2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermoController : ControllerBase
    {
        private readonly EPDbContext _context;

        public TermoController(EPDbContext context)
        {
            _context = context;
        }

        // GET: api/Termoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Termo>>> GetTermos()
        {
            return await _context.Termos.ToListAsync();
        }

        // GET: api/Termoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Termo>> GetTermo(string id)
        {
            var termo = await _context.Termos.FindAsync(id);

            if (termo == null)
            {
                return NotFound();
            }

            return termo;
        }

        // PUT: api/Termoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTermo(string id, Termo termo)
        {
            if (id != termo.TermoNome)
            {
                return BadRequest();
            }

            _context.Entry(termo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TermoExists(id))
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

        // POST: api/Termoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Termo>> PostTermo(Termo termo)
        {
            _context.Termos.Add(termo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TermoExists(termo.TermoNome))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTermo", new { id = termo.TermoNome }, termo);
        }

        // DELETE: api/Termoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTermo(string id)
        {
            var termo = await _context.Termos.FindAsync(id);
            if (termo == null)
            {
                return NotFound();
            }

            _context.Termos.Remove(termo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TermoExists(string id)
        {
            return _context.Termos.Any(e => e.TermoNome == id);
        }
    }
}
