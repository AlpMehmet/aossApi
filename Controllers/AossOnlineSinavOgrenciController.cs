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
    public class AossOnlineSinavOgrenciController : ControllerBase
    {
        private readonly AossContext _context;

        public AossOnlineSinavOgrenciController(AossContext context)
        {
            _context = context;
        }

        // GET: api/AossOnlineSinavOgrenci
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AossOnlineSinavOgrenci>>> GetAossOnlineSinavOgrenci()
        {
            return await _context.AossOnlineSinavOgrenci.ToListAsync();
        }

        // GET: api/AossOnlineSinavOgrenci/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AossOnlineSinavOgrenci>> GetAossOnlineSinavOgrenci(long id)
        {
            var aossOnlineSinavOgrenci = await _context.AossOnlineSinavOgrenci.FindAsync(id);

            if (aossOnlineSinavOgrenci == null)
            {
                return NotFound();
            }

            return aossOnlineSinavOgrenci;
        }

        // PUT: api/AossOnlineSinavOgrenci/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAossOnlineSinavOgrenci(long id, AossOnlineSinavOgrenci aossOnlineSinavOgrenci)
        {
            if (id != aossOnlineSinavOgrenci.Id)
            {
                return BadRequest();
            }

            _context.Entry(aossOnlineSinavOgrenci).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossOnlineSinavOgrenciExists(id))
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

        // POST: api/AossOnlineSinavOgrenci
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AossOnlineSinavOgrenci>> PostAossOnlineSinavOgrenci(AossOnlineSinavOgrenci aossOnlineSinavOgrenci)
        {
            _context.AossOnlineSinavOgrenci.Add(aossOnlineSinavOgrenci);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAossOnlineSinavOgrenci", new { id = aossOnlineSinavOgrenci.Id }, aossOnlineSinavOgrenci);
        }

        // DELETE: api/AossOnlineSinavOgrenci/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AossOnlineSinavOgrenci>> DeleteAossOnlineSinavOgrenci(long id)
        {
            var aossOnlineSinavOgrenci = await _context.AossOnlineSinavOgrenci.FindAsync(id);
            if (aossOnlineSinavOgrenci == null)
            {
                return NotFound();
            }

            _context.AossOnlineSinavOgrenci.Remove(aossOnlineSinavOgrenci);
            await _context.SaveChangesAsync();

            return aossOnlineSinavOgrenci;
        }

        private bool AossOnlineSinavOgrenciExists(long id)
        {
            return _context.AossOnlineSinavOgrenci.Any(e => e.Id == id);
        }
    }
}
