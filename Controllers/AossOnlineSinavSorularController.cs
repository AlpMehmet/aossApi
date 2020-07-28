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
    public class AossOnlineSinavSorularController : ControllerBase
    {
        private readonly AossContext _context;

        public AossOnlineSinavSorularController(AossContext context)
        {
            _context = context;
        }

        // GET: api/AossOnlineSinavSorular
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AossOnlineSinavSorular>>> GetAossOnlineSinavSorular()
        {
            return await _context.AossOnlineSinavSorular.ToListAsync();
        }

        // GET: api/AossOnlineSinavSorular/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AossOnlineSinavSorular>> GetAossOnlineSinavSorular(long id)
        {
            var aossOnlineSinavSorular = await _context.AossOnlineSinavSorular.FindAsync(id);

            if (aossOnlineSinavSorular == null)
            {
                return NotFound();
            }

            return aossOnlineSinavSorular;
        }

        // PUT: api/AossOnlineSinavSorular/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAossOnlineSinavSorular(long id, AossOnlineSinavSorular aossOnlineSinavSorular)
        {
            if (id != aossOnlineSinavSorular.Id)
            {
                return BadRequest();
            }

            _context.Entry(aossOnlineSinavSorular).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossOnlineSinavSorularExists(id))
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

        // POST: api/AossOnlineSinavSorular
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AossOnlineSinavSorular>> PostAossOnlineSinavSorular(AossOnlineSinavSorular aossOnlineSinavSorular)
        {
            _context.AossOnlineSinavSorular.Add(aossOnlineSinavSorular);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAossOnlineSinavSorular", new { id = aossOnlineSinavSorular.Id }, aossOnlineSinavSorular);
        }

        // DELETE: api/AossOnlineSinavSorular/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AossOnlineSinavSorular>> DeleteAossOnlineSinavSorular(long id)
        {
            var aossOnlineSinavSorular = await _context.AossOnlineSinavSorular.FindAsync(id);
            if (aossOnlineSinavSorular == null)
            {
                return NotFound();
            }

            _context.AossOnlineSinavSorular.Remove(aossOnlineSinavSorular);
            await _context.SaveChangesAsync();

            return aossOnlineSinavSorular;
        }

        private bool AossOnlineSinavSorularExists(long id)
        {
            return _context.AossOnlineSinavSorular.Any(e => e.Id == id);
        }
    }
}
