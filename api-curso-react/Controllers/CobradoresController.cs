using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_curso_react.Data;
using api_curso_react.Models;
using api_curso_react.Enumerados;

namespace api_curso_react.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CobradoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CobradoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cobradores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cobrador>>> GetCobradores()
        {
            return await _context.Cobradores.ToListAsync();
        }

        // GET: api/Cobradores/5
        [HttpGet("{TipoIdentificacion}/{Idenfificacion}")]
        public async Task<ActionResult<Cobrador>> GetCobrador(TipoIdentificacion TipoIdentificacion, string Idenfificacion)
        {
            var cobrador = await _context.Cobradores.FindAsync(TipoIdentificacion, Idenfificacion);

            if (cobrador == null)
            {
                return NotFound();
            }

            return cobrador;
        }

        // PUT: api/Cobradores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{TipoIdentificacion}/{Idenfificacion}")]
        public async Task<IActionResult> PutCobrador(TipoIdentificacion TipoIdentificacion, string Idenfificacion, Cobrador cobrador)
        {
            if (Idenfificacion != cobrador.Identificacion && TipoIdentificacion != cobrador.TipoIdentificacion)
            {
                return BadRequest();
            }            

            _context.Entry(cobrador).State = EntityState.Modified;

            try
            {
                cobrador.ModificadoPor = "SysUser";
                cobrador.Activo = true;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CobradorExists(TipoIdentificacion, Idenfificacion))
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

        // POST: api/Cobradores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cobrador>> PostCobrador(Cobrador cobrador)
        {
            cobrador.CreadoPor = "SysUser";

            _context.Cobradores.Add(cobrador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CobradorExists(cobrador.TipoIdentificacion, cobrador.Identificacion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCobrador", new { TipoIdentificacion = cobrador.TipoIdentificacion, Idenfificacion = cobrador.Identificacion }, cobrador);
        }

        // DELETE: api/Cobradores/5
        [HttpDelete("{TipoIdentificacion}/{Idenfificacion}")]
        public async Task<ActionResult<Cobrador>> DeleteCobrador(TipoIdentificacion TipoIdentificacion, string Idenfificacion)
        {
            var cobrador = await _context.Cobradores.FindAsync(TipoIdentificacion, Idenfificacion);
            if (cobrador == null)
            {
                return NotFound();
            }

            _context.Cobradores.Remove(cobrador);
            await _context.SaveChangesAsync();

            return cobrador;
        }

        private bool CobradorExists(TipoIdentificacion TipoIdentificacion, string Idenfificacion)
        {
            return _context.Cobradores.Any(e => e.Identificacion == Idenfificacion && e.TipoIdentificacion == TipoIdentificacion);
        }
    }
}
