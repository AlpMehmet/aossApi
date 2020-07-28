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
    public class AossSorularController : ControllerBase
    {
        private readonly AossContext _context;

        public AossSorularController(AossContext context)
        {
            _context = context;
        }

        // GET: api/AossSorular
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AossSorular>>> GetAossSorular()
        {
            return await _context.AossSorular.ToListAsync();
        }

        // GET: api/AossSorular/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AossSorular>> GetAossSorular(long id)
        {
            var aossSorular = await _context.AossSorular.FindAsync(id);

            if (aossSorular == null)
            {
                return NotFound();
            }

            return aossSorular;
        }

        // PUT: api/AossSorular/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAossSorular(long id, AossSorular aossSorular)
        {
            if (id != aossSorular.Id)
            {
                return BadRequest();
            }

            _context.Entry(aossSorular).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossSorularExists(id))
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

        // POST: api/AossSorular
        [HttpPost]
        public async Task<ActionResult<AossSorular>> PostAossSorular(AossSorular aossSorular)
        {
            _context.AossSorular.Add(aossSorular);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAossSorular), new { id = aossSorular.Id }, aossSorular);
        }

        // DELETE: api/AossSorular/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AossSorular>> DeleteAossSorular(long id)
        {
            var aossSorular = await _context.AossSorular.FindAsync(id);
            if (aossSorular == null)
            {
                return NotFound();
            }

            _context.AossSorular.Remove(aossSorular);
            await _context.SaveChangesAsync();

            return aossSorular;
        }

        private bool AossSorularExists(long id)
        {
            return _context.AossSorular.Any(e => e.Id == id);
        }
    }
}
