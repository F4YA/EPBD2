using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EPBD2.Models;

namespace EPBD2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditalController : ControllerBase
    {
        private readonly EPDbContext _context;

        public EditalController(EPDbContext context)
        {
            _context = context;
        }

        // GET: api/Edital
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Edital>>> GetEditais()
        {
            return await _context.Editais.ToListAsync();
        }

        // GET: api/Edital/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Edital>> GetEdital(string id)
        {
            var edital = await _context.Editais.FindAsync(id);

            if (edital == null)
            {
                return NotFound();
            }

            return edital;
        }

        // PUT: api/Edital/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEdital(string id, Edital edital)
        {
            if (id != edital.Nome)
            {
                return BadRequest();
            }

            _context.Entry(edital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EditalExists(id))
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

        // POST: api/Edital
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Edital>> PostEdital(Edital edital)
        {
            _context.Editais.Add(edital);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EditalExists(edital.Nome))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEdital", new { id = edital.Nome }, edital);
        }

        // DELETE: api/Edital/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEdital(string id)
        {
            var edital = await _context.Editais.FindAsync(id);
            if (edital == null)
            {
                return NotFound();
            }

            _context.Editais.Remove(edital);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EditalExists(string id)
        {
            return _context.Editais.Any(e => e.Nome == id);
        }
    }
}
