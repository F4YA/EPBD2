using EPBD2.DTOs;
using EPBD2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EPBD2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OscController : ControllerBase
    {
        private readonly EPDbContext _context;

        public OscController(EPDbContext context)
        {
            _context = context;
        }

        // GET: api/<OscController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var oscs = await _context.Oscs
                                     .Include(o => o.ContatosOsc)
                                     .Include(o => o.RepresentantesLegais)
                                     .Include(o => o.LocalizacoesOsc)
                                     .ToListAsync();
            return Ok(oscs);
        }

        // GET api/<OscController>/5
        [HttpGet("{cnpj}")]
        public async Task<IActionResult> Get(string cnpj)
        {
            var osc = await _context.Oscs
                                    .Include(o => o.ContatosOsc)
                                    .Include(o => o.RepresentantesLegais)
                                    .Include(o => o.LocalizacoesOsc)
                                    .FirstOrDefaultAsync(o => o.Cnpj == cnpj);

            if (osc == null)
            {
                return NotFound($"OSC com CNPJ {cnpj} não encontrada.");
            }

            return Ok(new NewOscDTO(osc));
        }

        // POST api/<OscController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewOscDTO value)
        {
            if (value.ContatosOsc == null || !value.ContatosOsc.Any())
            {
                return BadRequest("A OSC deve ter pelo menos um contato.");
            }

            if (value.RepresentantesLegais == null || !value.RepresentantesLegais.Any())
            {
                return BadRequest("A OSC deve ter pelo menos um representante legal.");
            }

            if (value.LocalizacoesOsc == null || !value.LocalizacoesOsc.Any())
            {
                return BadRequest("A OSC deve ter pelo menos uma localização.");
            }

            // Verificar se o CNPJ já existe
            if (await _context.Oscs.AnyAsync(o => o.Cnpj == value.Cnpj))
            {
                return Conflict($"Já existe uma OSC com o CNPJ {value.Cnpj}.");
            }

            // Verificar se os CPFs dos representantes legais já existem para outras OSCs
            foreach (var rep in value.RepresentantesLegais)
            {
                if (await _context.RepresentanteLegais.AnyAsync(r => r.Cpf == rep.Cpf))
                {
                    return Conflict($"O CPF {rep.Cpf} do representante legal já está cadastrado para outra entidade.");
                }
            }

            // Verificar se os contatos de OSC já existem
            foreach (var contato in value.ContatosOsc)
            {
                if (await _context.ContatoOscs.AnyAsync(c => c.Contato == contato.Contato))
                {
                    return Conflict($"O contato '{contato.Contato}' já está cadastrado para outra OSC.");
                }
            }

            _context.Oscs.Add(value.ConvertToEntity());
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { cnpj = value.Cnpj }, value);
        }

        // PUT api/<OscController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Osc value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingOsc = await _context.Oscs
                                            .Include(o => o.ContatosOsc)
                                            .Include(o => o.RepresentantesLegais)
                                            .Include(o => o.LocalizacoesOsc)
                                            .FirstOrDefaultAsync(o => o.Cnpj == value.Cnpj);

            if (existingOsc == null)
            {
                return NotFound($"OSC com CNPJ {value.Cnpj} não encontrada.");
            }

            // Atualizar propriedades da OSC
            _context.Entry(existingOsc).CurrentValues.SetValues(value);

            // Validações e Sincronização de ContatosOsc
            if (value.ContatosOsc == null || !value.ContatosOsc.Any())
            {
                return BadRequest("A OSC deve ter pelo menos um contato.");
            }
            var contatosToRemove = existingOsc.ContatosOsc.Where(c => !value.ContatosOsc.Any(vc => vc.Id == c.Id)).ToList();
            if (contatosToRemove.Count == existingOsc.ContatosOsc.Count && existingOsc.ContatosOsc.Count > 0)
            {
                return BadRequest("Não é possível remover todos os contatos se houver apenas um.");
            }
            foreach (var contato in contatosToRemove)
            {
                _context.ContatoOscs.Remove(contato);
            }
            foreach (var newContato in value.ContatosOsc)
            {
                var existingContato = existingOsc.ContatosOsc.FirstOrDefault(c => c.Id == newContato.Id);
                if (existingContato != null)
                {
                    _context.Entry(existingContato).CurrentValues.SetValues(newContato);
                }
                else
                {
                    // Validação de contato único para novos contatos
                    if (await _context.ContatoOscs.AnyAsync(c => c.Contato == newContato.Contato))
                    {
                        return Conflict($"O contato '{newContato.Contato}' já está cadastrado.");
                    }
                    newContato.Osc = value;
                    _context.ContatoOscs.Add(newContato);
                }
            }


            // Validações e Sincronização de RepresentantesLegais
            if (value.RepresentantesLegais == null || !value.RepresentantesLegais.Any())
            {
                return BadRequest("A OSC deve ter pelo menos um representante legal.");
            }
            var repsToRemove = existingOsc.RepresentantesLegais.Where(r => !value.RepresentantesLegais.Any(vr => vr.Cpf == r.Cpf)).ToList();
            if (repsToRemove.Count == existingOsc.RepresentantesLegais.Count && existingOsc.RepresentantesLegais.Count > 0)
            {
                return BadRequest("Não é possível remover todos os representantes legais se houver apenas um.");
            }
            foreach (var rep in repsToRemove)
            {
                _context.RepresentanteLegais.Remove(rep);
            }
            foreach (var newRep in value.RepresentantesLegais)
            {
                var existingRep = existingOsc.RepresentantesLegais.FirstOrDefault(r => r.Cpf == newRep.Cpf);
                if (existingRep != null)
                {
                    _context.Entry(existingRep).CurrentValues.SetValues(newRep);
                }
                else
                {
                    // Validação de CPF de representante legal único para novos representantes
                    if (await _context.RepresentanteLegais.AnyAsync(r => r.Cpf == newRep.Cpf))
                    {
                        return Conflict($"O CPF {newRep.Cpf} do representante legal já está cadastrado para outra entidade.");
                    }
                    newRep.Osc = value;
                    _context.RepresentanteLegais.Add(newRep);
                }
            }

            // Validações e Sincronização de LocalizacoesOsc
            if (value.LocalizacoesOsc == null || !value.LocalizacoesOsc.Any())
            {
                return BadRequest("A OSC deve ter pelo menos uma localização.");
            }
            var locsToRemove = existingOsc.LocalizacoesOsc.Where(l => !value.LocalizacoesOsc.Any(vl => vl.Id == l.Id)).ToList();
            if (locsToRemove.Count == existingOsc.LocalizacoesOsc.Count && existingOsc.LocalizacoesOsc.Count > 0)
            {
                return BadRequest("Não é possível remover todas as localizações se houver apenas uma.");
            }
            foreach (var loc in locsToRemove)
            {
                _context.LocalizacoesOscs.Remove(loc);
            }
            foreach (var newLoc in value.LocalizacoesOsc)
            {
                var existingLoc = existingOsc.LocalizacoesOsc.FirstOrDefault(l => l.Id == newLoc.Id);
                if (existingLoc != null)
                {
                    _context.Entry(existingLoc).CurrentValues.SetValues(newLoc);
                }
                else
                {
                    newLoc.Osc = value;
                    _context.LocalizacoesOscs.Add(newLoc);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Oscs.Any(e => e.Cnpj == value.Cnpj))
                {
                    return NotFound($"OSC com CNPJ {value.Cnpj} não encontrada.");
                }
                throw;
            }

            return NoContent(); // 204 No Content
        }

        // DELETE api/<OscController>/5
        [HttpDelete("{cnpj}")]
        public async Task<IActionResult> Delete(string cnpj)
        {
            //var osc = await _context.Oscs.FindAsync(cnpj);

            var osc = await _context.Oscs
                                    .Include(o => o.ContatosOsc)
                                    .Include(o => o.RepresentantesLegais)
                                    .Include(o => o.LocalizacoesOsc)
                                    .FirstOrDefaultAsync(o => o.Cnpj == cnpj);

            if (osc == null)
            {
                return NotFound($"OSC com CNPJ {cnpj} não encontrada.");
            }

            _context.Oscs.Remove(osc);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content
        }
    }
}