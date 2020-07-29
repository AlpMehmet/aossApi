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
    public class AossHocaController : ControllerBase
    {
        private readonly AossContext _context;

        public AossHocaController(AossContext context)
        {
            _context = context;
        }

        // GET: api/AossHoca
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AossHoca>>> GetAossHoca()
        {
            return await _context.AossHoca.ToListAsync();
        }

        // GET: api/AossHoca/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AossHoca>> GetAossHoca(long id)
        {
            var aossHoca = await _context.AossHoca.FindAsync(id);

            if (aossHoca == null)
            {
                return NotFound();
            }

            return aossHoca;
        }

        // PUT: api/AossHoca/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAossHoca(long id, AossHoca aossHoca)
        {
            if (id != aossHoca.Id)
            {
                return BadRequest();
            }

            _context.Entry(aossHoca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossHocaExists(id))
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

        // POST: api/AossHoca
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AossHoca>> PostAossHoca(AossHoca aossHoca)
        {
            _context.AossHoca.Add(aossHoca);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAossHoca", new { id = aossHoca.Id }, aossHoca);
        }

        // DELETE: api/AossHoca/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AossHoca>> DeleteAossHoca(long id)
        {
            var aossHoca = await _context.AossHoca.FindAsync(id);
            if (aossHoca == null)
            {
                return NotFound();
            }

            _context.AossHoca.Remove(aossHoca);
            await _context.SaveChangesAsync();

            return aossHoca;
        }

        private bool AossHocaExists(long id)
        {
            return _context.AossHoca.Any(e => e.Id == id);
        }
    }
}
