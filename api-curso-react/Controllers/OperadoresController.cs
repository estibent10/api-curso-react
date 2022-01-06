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
    public class OperadoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OperadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Operadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operador>>> GetOperadores()
        {
            return await _context.Operadores.ToListAsync();
        }

        // GET: api/Operadores/1/5
        [HttpGet("{TipoIdentificacion}/{Idenfificacion}")]
        public async Task<ActionResult<Operador>> GetOperador(TipoIdentificacion TipoIdentificacion, string Idenfificacion)
        {
            var operador = await _context.Operadores.FindAsync(TipoIdentificacion, Idenfificacion);

            if (operador == null)
            {
                return NotFound();
            }

            return operador;
        }

        // PUT: api/Operadores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{TipoIdentificacion}/{Idenfificacion}")]
        public async Task<IActionResult> PutOperador(TipoIdentificacion TipoIdentificacion, string Idenfificacion, Operador operador)
        {
            if (Idenfificacion != operador.Identificacion && TipoIdentificacion != operador.TipoIdentificacion)
            {
                return BadRequest();
            }

            _context.Entry(operador).State = EntityState.Modified;

            try
            {
                operador.ModificadoPor = "SysUser";
                operador.Activo = true;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperadorExists(TipoIdentificacion, Idenfificacion))
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

        // POST: api/Operadores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Operador>> PostOperador(Operador operador)
        {
            operador.CreadoPor = "SysUser";

            _context.Operadores.Add(operador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OperadorExists(operador.TipoIdentificacion, operador.Identificacion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            
            return CreatedAtAction("GetOperador", new { TipoIdentificacion = operador.TipoIdentificacion, Idenfificacion = operador.Identificacion }, operador);
        }

        // DELETE: api/Operadores/5
        [HttpDelete("{TipoIdentificacion}/{Idenfificacion}")]
        public async Task<ActionResult<Operador>> DeleteOperador(TipoIdentificacion TipoIdentificacion, string Idenfificacion)
        {
            var operador = await _context.Operadores.FindAsync(TipoIdentificacion, Idenfificacion);
            if (operador == null)
            {
                return NotFound();
            }

            _context.Operadores.Remove(operador);
            await _context.SaveChangesAsync();

            return operador;
        }

        private bool OperadorExists(TipoIdentificacion TipoIdentificacion, string Idenfificacion)
        {
            return _context.Operadores.Any(e => e.Identificacion == Idenfificacion && e.TipoIdentificacion == TipoIdentificacion);
        }
    }
}
