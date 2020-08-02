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
    public class AossHataliSorularController : ControllerBase
    {
        private readonly AossContext _context;

        public AossHataliSorularController(AossContext context)
        {
            _context = context;
        }
        // GET: api/AossHataliSorular
        [HttpGet("{alan}")]
        public ActionResult<IEnumerable<AossSorular>> GetAossHataliSorularSoru(string Alan)
        {
            List<AossHataliSorular> hataliSorular = _context.AossHataliSorular.Where(x=> x.Alani==Alan).ToList();
            List<AossSorular> sorular = new List<AossSorular>();
            foreach (var hatali in hataliSorular)
            {
                sorular.Add( _context.AossSorular.Where(x => x.Id == hatali.SoruId).FirstOrDefault());
            } 
            
            return  sorular;
        }

        [HttpPut("hatali/{id}")]
         public async Task<IActionResult> PutAossHataliSorularHatali(long id)
         {
            AossSorular hataliSoru0 = _context.AossSorular.Where(x => x.Id == id).FirstOrDefault();
            AossHataliSorular aossHataliSorular  = new AossHataliSorular(); 
            AossHataliSorular hataliSoru = _context.AossHataliSorular.Where(x => x.SoruId == hataliSoru0.Id).FirstOrDefault();
            int kacHoca = _context.AossHoca.Where(x => x.Alani == hataliSoru.Alani).Count();
            int toplamOyVeren = hataliSoru.oylamaHatali+hataliSoru.oylamaHatasiz;
            
            aossHataliSorular.Id=hataliSoru.Id;
            aossHataliSorular.SoruId=hataliSoru.SoruId;
            aossHataliSorular.Alani=hataliSoru.Alani;
            aossHataliSorular.oylamaHatali=hataliSoru.oylamaHatali+1;
            aossHataliSorular.oylamaHatasiz=hataliSoru.oylamaHatasiz;
            if(toplamOyVeren+1==kacHoca){
                if(hataliSoru.oylamaHatali+1-hataliSoru.oylamaHatasiz<=0){
                    aossHataliSorular.hataliMi=true;
                }
                else{
                    aossHataliSorular.hataliMi=false;
                }
            }
            _context.Entry(aossHataliSorular).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossHataliSorularExists(id))
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
        [HttpPut("hatasiz/{id}")]
         public async Task<IActionResult> PutAossHataliSorularHatasiz(long id )
         {
            AossSorular hataliSoru0 = _context.AossSorular.Where(x => x.Id == id).FirstOrDefault();
            AossHataliSorular aossHataliSorular  = new AossHataliSorular(); 
            AossHataliSorular hataliSoru = _context.AossHataliSorular.Where(x => x.SoruId == hataliSoru0.Id).FirstOrDefault();
            int kacHoca = _context.AossHoca.Where(x => x.Alani == hataliSoru.Alani).Count();
            int toplamOyVeren = hataliSoru.oylamaHatali+hataliSoru.oylamaHatasiz;
            
            aossHataliSorular.Id=hataliSoru.Id;
            aossHataliSorular.SoruId=hataliSoru.SoruId;
            aossHataliSorular.Alani=hataliSoru.Alani;
            aossHataliSorular.oylamaHatali=hataliSoru.oylamaHatali;
            aossHataliSorular.oylamaHatasiz=hataliSoru.oylamaHatasiz+1;
            if(toplamOyVeren+1==kacHoca){
                if(hataliSoru.oylamaHatali-hataliSoru.oylamaHatasiz+1<=0){
                    aossHataliSorular.hataliMi=true;
                }
                else{
                    aossHataliSorular.hataliMi=false;
                }
            }
            _context.Entry(aossHataliSorular).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossHataliSorularExists(id))
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

    
        [HttpPost]
        public async Task<ActionResult<AossHataliSorular>> PostAossHataliSorular(AossHataliSorular aossHataliSorular)
        {
            _context.AossHataliSorular.Add(aossHataliSorular);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAossHataliSorular", new { id = aossHataliSorular.Id }, aossHataliSorular);
        }
        // DELETE: api/AossHataliSorular/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AossHataliSorular>> DeleteAossHataliSorular(long id)
        {
            var aossHataliSorular = await _context.AossHataliSorular.FindAsync(id);
            if (aossHataliSorular == null)
            {
                return NotFound();
            }
            _context.AossHataliSorular.Remove(aossHataliSorular);
            await _context.SaveChangesAsync();
            return aossHataliSorular;
        }
        private bool AossHataliSorularExists(long id)
        {
            return _context.AossHataliSorular.Any(e => e.Id == id);
        }
    }
}
