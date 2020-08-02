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
    public class AossOnlineSinavOgrenciSorularController : ControllerBase
    {
        private readonly AossContext _context;

        public AossOnlineSinavOgrenciSorularController(AossContext context)
        {
            _context = context;
        }

        // GET: api/AossOnlineSinavOgrenciSorular
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AossOnlineSinavOgrenciSorular>>> GetAossOnlineSinavOgrenciSorular()
        {
            return await _context.AossOnlineSinavOgrenciSorular.ToListAsync();
        }

        // GET: api/AossOnlineSinavOgrenciSorular/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AossOnlineSinavOgrenciSorular>> GetAossOnlineSinavOgrenciSorular(long id)
        {
            var aossOnlineSinavOgrenciSorular = await _context.AossOnlineSinavOgrenciSorular.FindAsync(id);

            if (aossOnlineSinavOgrenciSorular == null)
            {
                return NotFound();
            }

            return aossOnlineSinavOgrenciSorular;
        }

        // PUT: api/AossOnlineSinavOgrenciSorular/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAossOnlineSinavOgrenciSorular(long id, AossOnlineSinavOgrenciSorular aossOnlineSinavOgrenciSorular)
        {
            if (id != aossOnlineSinavOgrenciSorular.Id)
            {
                return BadRequest();
            }

            _context.Entry(aossOnlineSinavOgrenciSorular).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossOnlineSinavOgrenciSorularExists(id))
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

        // POST: api/AossOnlineSinavOgrenciSorular
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AossOnlineSinavOgrenciSorular>> PostAossOnlineSinavOgrenciSorular(AossOnlineSinavOgrenciSorular aossOnlineSinavOgrenciSorular)
        {
            _context.AossOnlineSinavOgrenciSorular.Add(aossOnlineSinavOgrenciSorular);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAossOnlineSinavOgrenciSorular", new { id = aossOnlineSinavOgrenciSorular.Id }, aossOnlineSinavOgrenciSorular);
        }

        // DELETE: api/AossOnlineSinavOgrenciSorular/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AossOnlineSinavOgrenciSorular>> DeleteAossOnlineSinavOgrenciSorular(long id)
        {
            var aossOnlineSinavOgrenciSorular = await _context.AossOnlineSinavOgrenciSorular.FindAsync(id);
            if (aossOnlineSinavOgrenciSorular == null)
            {
                return NotFound();
            }

            _context.AossOnlineSinavOgrenciSorular.Remove(aossOnlineSinavOgrenciSorular);
            await _context.SaveChangesAsync();

            return aossOnlineSinavOgrenciSorular;
        }

        private bool AossOnlineSinavOgrenciSorularExists(long id)
        {
            return _context.AossOnlineSinavOgrenciSorular.Any(e => e.Id == id);
        }
    }
}
