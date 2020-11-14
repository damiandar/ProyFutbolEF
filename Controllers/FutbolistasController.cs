using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyFutbol.Models;

namespace ProyFutbol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FutbolistasController : ControllerBase
    {
        private readonly FutbolDbContext _context;

        public FutbolistasController(FutbolDbContext context)
        {
            _context = context;
        }

        // GET: api/Futbolistas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FutbolistaDto>>> GetFutbolistas()
        {
            //var posiciones=
            return await _context.Futbolistas.Include(x => x.Pais)
            .Include(f=> f.Equipo)
            .Include(c=>c.Contrato)
            .Select(z=> new FutbolistaDto()
                {
                    FutbolistaId=z.FutbolistaId,
                    Nombre=z.Nombre,
                    Edad=z.Edad,
                    Pais=z.Pais.Nombre,
                    Descripcion=z.Descripcion,
                    Equipo=z.Equipo.Nombre,
                    //Posiciones=_context.FutbolistaPosiciones.Where(x=> x.FutbolistaId==z.FutbolistaId).Select(x=> x.Posicion).ToList()
                    //Posiciones=z.FutbolistaPosiciones.Where(x=> x.FutbolistaId==z.FutbolistaId).Select(o=> o.Posicion).ToList()
                      PosDto=z.FutbolistaPosiciones.Where(x=> x.FutbolistaId==z.FutbolistaId).Select(o=> new PosicionDto()
                      { Id=o.PosicionId, Nombre=o.Posicion.Nombre}).ToList(),
                    Contrato=z.Contrato
                }
            )
            .ToListAsync();
        }


        // GET: api/Futbolistas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FutbolistaDto>> GetFutbolista(int id)
        {
            var futbolista = await _context.Futbolistas.Include("Pais")
            .Select(z=>new FutbolistaDto(){
                    FutbolistaId=z.FutbolistaId,
                    Nombre=z.Nombre,
                    Edad=z.Edad,
                    Pais=z.Pais.Nombre,
                    Descripcion=z.Descripcion,
                    Equipo=z.Equipo.Nombre,
                    PosDto=z.FutbolistaPosiciones.Where(x=> x.FutbolistaId==z.FutbolistaId).Select(o=> new PosicionDto()
                      { Id=o.PosicionId, Nombre=o.Posicion.Nombre}).ToList(),
                    Contrato=z.Contrato
            } )
            .Where(j=>j.FutbolistaId==id).FirstOrDefaultAsync<FutbolistaDto>();
           
            if (futbolista == null)
            {
                return NotFound();
            }


            return futbolista;
        }

        // PUT: api/Futbolistas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFutbolista(int id, Futbolista futbolista)
        {
            if (id != futbolista.FutbolistaId)
            {
                return BadRequest();
            }

            _context.Entry(futbolista).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FutbolistaExists(id))
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

        // POST: api/Futbolistas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Futbolista>> PostFutbolista(Futbolista futbolista)
        {
            _context.Futbolistas.Add(futbolista);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFutbolista", new { id = futbolista.FutbolistaId }, futbolista);
        }

        // DELETE: api/Futbolistas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Futbolista>> DeleteFutbolista(int id)
        {
            var futbolista = await _context.Futbolistas.FindAsync(id);
            if (futbolista == null)
            {
                return NotFound();
            }

            _context.Futbolistas.Remove(futbolista);
            await _context.SaveChangesAsync();

            return futbolista;
        }

        private bool FutbolistaExists(int id)
        {
            return _context.Futbolistas.Any(e => e.FutbolistaId == id);
        }
    }
}
