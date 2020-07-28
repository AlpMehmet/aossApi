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
    public class AossOnlineSinavController : ControllerBase
    {
        private readonly AossContext _context;

        public AossOnlineSinavController(AossContext context)
        {
            _context = context;
        }

        // GET: api/AossOnlineSinav
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AossOnlineSinav>>> GetAossOnlineSinav()
        {
            return await _context.AossOnlineSinav.ToListAsync();
        }

        // GET: api/AossOnlineSinav/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AossOnlineSinav>> GetAossOnlineSinav(long id)
        {
            var aossOnlineSinav = await _context.AossOnlineSinav.FindAsync(id);

            if (aossOnlineSinav == null)
            {
                return NotFound();
            }

            return aossOnlineSinav;
        }

        // PUT: api/AossOnlineSinav/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAossOnlineSinav(long id, AossOnlineSinav aossOnlineSinav)
        {
            if (id != aossOnlineSinav.Id)
            {
                return BadRequest();
            }

            _context.Entry(aossOnlineSinav).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossOnlineSinavExists(id))
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

        // POST: api/AossOnlineSinav
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AossOnlineSinav>> PostAossOnlineSinav(AossOnlineSinav aossOnlineSinav)
        {
            _context.AossOnlineSinav.Add(aossOnlineSinav);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAossOnlineSinav", new { id = aossOnlineSinav.Id }, aossOnlineSinav);
        }

        // DELETE: api/AossOnlineSinav/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AossOnlineSinav>> DeleteAossOnlineSinav(long id)
        {
            var aossOnlineSinav = await _context.AossOnlineSinav.FindAsync(id);
            if (aossOnlineSinav == null)
            {
                return NotFound();
            }

            _context.AossOnlineSinav.Remove(aossOnlineSinav);
            await _context.SaveChangesAsync();

            return aossOnlineSinav;
        }

        private bool AossOnlineSinavExists(long id)
        {
            return _context.AossOnlineSinav.Any(e => e.Id == id);
        }
    }
}
