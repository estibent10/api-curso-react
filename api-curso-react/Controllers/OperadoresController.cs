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

        // GET: api/Operadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Operador>> GetOperador(string id)
        {
            var operador = await _context.Operadores.FindAsync(id);

            if (operador == null)
            {
                return NotFound();
            }

            return operador;
        }

        // PUT: api/Operadores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperador(string id, Operador operador)
        {
            if (id != operador.Identificacion)
            {
                return BadRequest();
            }

            _context.Entry(operador).State = EntityState.Modified;

            try
            {
                operador.ModificadoPor = "diazgs";
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperadorExists(id))
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
            operador.CreadoPor = "diazgs";

            _context.Operadores.Add(operador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OperadorExists(operador.Identificacion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOperador", new { id = operador.Identificacion }, operador);
        }

        // DELETE: api/Operadores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Operador>> DeleteOperador(string id)
        {
            var operador = await _context.Operadores.FindAsync(id);
            if (operador == null)
            {
                return NotFound();
            }

            _context.Operadores.Remove(operador);
            await _context.SaveChangesAsync();

            return operador;
        }

        private bool OperadorExists(string id)
        {
            return _context.Operadores.Any(e => e.Identificacion == id);
        }
    }
}
