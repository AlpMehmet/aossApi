using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AossAPI.Models;
using AossApi.Models;

namespace AossAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AossZorlukPuanlamaController : ControllerBase
    {
        private readonly AossContext _context;

        public AossZorlukPuanlamaController(AossContext context)
        {
            _context = context;
        }

        // GET: api/AossZorlukPuanlama
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AossZorlukPuanlama>>> GetAossZorlukPuanlama()
        {
            return await _context.AossZorlukPuanlama.ToListAsync();
        }

        // GET: api/AossZorlukPuanlama/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AossZorlukPuanlama>> GetAossZorlukPuanlama(long id)
        {
            var aossZorlukPuanlama = await _context.AossZorlukPuanlama.FindAsync(id);

            if (aossZorlukPuanlama == null)
            {
                return NotFound();
            }

            return aossZorlukPuanlama;
        }

        // PUT: api/AossZorlukPuanlama/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAossZorlukPuanlama(long id, AossZorlukPuanlama aossZorlukPuanlama)
        {
            if (id != aossZorlukPuanlama.Id)
            {
                return BadRequest();
            }

            _context.Entry(aossZorlukPuanlama).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossZorlukPuanlamaExists(id))
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

        // POST: api/AossZorlukPuanlama
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AossZorlukPuanlama>> PostAossZorlukPuanlama(AossZorlukPuanlama aossZorlukPuanlama)
        {
            _context.AossZorlukPuanlama.Add(aossZorlukPuanlama);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAossZorlukPuanlama", new { id = aossZorlukPuanlama.Id }, aossZorlukPuanlama);
        }

        // DELETE: api/AossZorlukPuanlama/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AossZorlukPuanlama>> DeleteAossZorlukPuanlama(long id)
        {
            var aossZorlukPuanlama = await _context.AossZorlukPuanlama.FindAsync(id);
            if (aossZorlukPuanlama == null)
            {
                return NotFound();
            }

            _context.AossZorlukPuanlama.Remove(aossZorlukPuanlama);
            await _context.SaveChangesAsync();

            return aossZorlukPuanlama;
        }

        private bool AossZorlukPuanlamaExists(long id)
        {
            return _context.AossZorlukPuanlama.Any(e => e.Id == id);
        }
    }
}
