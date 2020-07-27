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
    public class AossOgrenciController : ControllerBase
    {
        private readonly AossContext _context;

        public AossOgrenciController(AossContext context)
        {
            _context = context;
        }

        // GET: api/AossOgrenci
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AossOgrenci>>> GetAossOgrenci()
        {
            return await _context.AossOgrenci.ToListAsync();
        }

        // GET: api/AossOgrenci/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AossOgrenci>> GetAossOgrenci(long id)
        {
            var aossOgrenci = await _context.AossOgrenci.FindAsync(id);

            if (aossOgrenci == null)
            {
                return NotFound();
            }

            return aossOgrenci;
        }
        [HttpGet("{OgrenciNo}/{Sifre}")]
        public long Giris(int OgrenciNo, string Sifre)
        {
            AossOgrenci a = _context.AossOgrenci.Where(x => x.OgrenciNo == OgrenciNo && x.Sifre == Sifre).FirstOrDefault();
            if(a==null){
                return -1;
            }
            else{
                return a.Id;
            }
        }    
        // PUT: api/AossOgrenci/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAossOgrenci(long id, AossOgrenci aossOgrenci)
        {
            if (id != aossOgrenci.Id)
            {
                return BadRequest();
            }

            _context.Entry(aossOgrenci).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossOgrenciExists(id))
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

        // POST: api/AossOgrenci
        [HttpPost]
        public async Task<ActionResult<AossOgrenci>> PostAossOgrenci(AossOgrenci aossOgrenci)
        {
            _context.AossOgrenci.Add(aossOgrenci);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAossOgrenci", new { id = aossOgrenci.Id }, aossOgrenci);
        }

        // DELETE: api/AossOgrenci/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AossOgrenci>> DeleteAossOgrenci(long id)
        {
            var aossOgrenci = await _context.AossOgrenci.FindAsync(id);
            if (aossOgrenci == null)
            {
                return NotFound();
            }

            _context.AossOgrenci.Remove(aossOgrenci);
            await _context.SaveChangesAsync();

            return aossOgrenci;
        }

        private bool AossOgrenciExists(long id)
        {
            return _context.AossOgrenci.Any(e => e.Id == id);
        }
    }
}
