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
    public class AossYoneticiController : ControllerBase
    {
        private readonly AossContext _context;

        public AossYoneticiController(AossContext context)
        {
            _context = context;
        }

        // GET: api/AossYonetici
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AossYonetici>>> GetAossYonetici()
        {
            return await _context.AossYonetici.ToListAsync();
        }

        // GET: api/AossYonetici/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AossYonetici>> GetAossYonetici(long id)
        {
            var aossYonetici = await _context.AossYonetici.FindAsync(id);

            if (aossYonetici == null)
            {
                return NotFound();
            }

            return aossYonetici;
        }
        [HttpGet("{Mail}/{Sifre}")]
        public long Giris(string Mail, string Sifre)
        {
            AossYonetici a = _context.AossYonetici.Where(x => x.Mail == Mail && x.Sifre == Sifre).FirstOrDefault();
            if(a==null){
                return -1;
            }
            else{
                return a.Id;
            }
        }  
        // PUT: api/AossYonetici/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAossYonetici(long id, AossYonetici aossYonetici)
        {
            if (id != aossYonetici.Id)
            {
                return BadRequest();
            }

            _context.Entry(aossYonetici).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossYoneticiExists(id))
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

        // POST: api/AossYonetici
        [HttpPost]
        public async Task<ActionResult<AossYonetici>> PostAossYonetici(AossYonetici aossYonetici)
        {
            _context.AossYonetici.Add(aossYonetici);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAossYonetici", new { id = aossYonetici.Id }, aossYonetici);
        }

        // DELETE: api/AossYonetici/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AossYonetici>> DeleteAossYonetici(long id)
        {
            var aossYonetici = await _context.AossYonetici.FindAsync(id);
            if (aossYonetici == null)
            {
                return NotFound();
            }

            _context.AossYonetici.Remove(aossYonetici);
            await _context.SaveChangesAsync();

            return aossYonetici;
        }

        private bool AossYoneticiExists(long id)
        {
            return _context.AossYonetici.Any(e => e.Id == id);
        }
    }
}
