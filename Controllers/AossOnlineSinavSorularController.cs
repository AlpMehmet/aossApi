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
            float yeniGelen = 0 , toplamPuan = 0;
            int kolayS = 0, zorS = 0, ortaS = 0, diziBoyut=0,dizidesira=0;            
            AossZorlukPuanlama seciliSinavZorluk = _context.AossZorlukPuanlama.Where(x => x.OnlineSinavId == aossOnlineSinavSorular.OnlineSinavId).FirstOrDefault();
            List<AossOnlineSinavSorular> sorular = _context.AossOnlineSinavSorular.Where(x => x.OnlineSinavId == aossOnlineSinavSorular.OnlineSinavId).ToList();

            AossSorular eklenmekIstenen = _context.AossSorular.Where(x => x.Id == aossOnlineSinavSorular.SoruId).FirstOrDefault();
            int kolaySoruSayisi = _context.AossSorular.Where(x => x.SoruAlani == eklenmekIstenen.SoruAlani && x.Zorluk == "KOLAY" ).Count();
            int ortaSoruSayisi = _context.AossSorular.Where(x => x.SoruAlani == eklenmekIstenen.SoruAlani && x.Zorluk == "ORTA" ).Count();
            int zorSoruSayisi = _context.AossSorular.Where(x => x.SoruAlani == eklenmekIstenen.SoruAlani && x.Zorluk == "ZOR" ).Count();
            
            
            if(eklenmekIstenen.Zorluk=="ZOR"){
                zorS+=1;
                   yeniGelen=seciliSinavZorluk.ZorPuan;
               }
            else if (eklenmekIstenen.Zorluk=="ORTA")
               {ortaS+=1;
                   yeniGelen=seciliSinavZorluk.OrtaPuan;
               }
            else if (eklenmekIstenen.Zorluk=="KOLAY")
               {
                   kolayS+=1;
                   yeniGelen=seciliSinavZorluk.KolayPuan;
               }
            foreach (var soru in sorular)
            {
               AossSorular seciliSoru = _context.AossSorular.Where(x => x.Id == soru.SoruId).FirstOrDefault();
               if(seciliSoru.Zorluk=="ZOR"){
                   zorS+=1;
                   toplamPuan+=seciliSinavZorluk.ZorPuan;
               }
               else if (seciliSoru.Zorluk=="ORTA")
               {ortaS+=1;
                   toplamPuan+=seciliSinavZorluk.OrtaPuan;
               }
               else if (seciliSoru.Zorluk=="KOLAY")
               {kolayS+=1;
                   toplamPuan+=seciliSinavZorluk.KolayPuan;
               }
            }
            for (int i = 0; i <= kolaySoruSayisi; i++)
            {
                for (int j = 0; j <= ortaSoruSayisi; j++)
                {
                    for (int k = 0; k <= zorSoruSayisi; k++)
                    {
                        if(i*seciliSinavZorluk.KolayPuan+j*seciliSinavZorluk.OrtaPuan+k*seciliSinavZorluk.ZorPuan==100){
                            diziBoyut++;
                        }
                    }
                }
            }
           float[,] kombinasyonlar = new float[diziBoyut, 3];
            for (int i = 0; i <= kolaySoruSayisi; i++)
            {
                for (int j = 0; j <= ortaSoruSayisi; j++)
                {
                    for (int k = 0; k <= zorSoruSayisi; k++)
                    {
                         if(i*seciliSinavZorluk.KolayPuan+j*seciliSinavZorluk.OrtaPuan+k*seciliSinavZorluk.ZorPuan==100){
                            
                            kombinasyonlar[dizidesira,0] = i;
                            kombinasyonlar[dizidesira,1] = j;
                            kombinasyonlar[dizidesira,2] = k;
                            dizidesira++;
                            
                        }
                    }
                }
            }
            if(toplamPuan+yeniGelen<=100)
            {
                if(eklenmekIstenen.Zorluk=="KOLAY"){
                    for (int i = 0; i < diziBoyut; i++)
                    {
                        if(kolayS<=kombinasyonlar[i,0]){
                          
                             _context.AossOnlineSinavSorular.Add(aossOnlineSinavSorular);
                             await _context.SaveChangesAsync();

                            return CreatedAtAction("GetAossOnlineSinavSorular", new { id = aossOnlineSinavSorular.Id }, aossOnlineSinavSorular);
                        }
                    }
                    throw new ArgumentException("Kolay Soru Ekleme Hakkınız Bitti");
                }
                else if(eklenmekIstenen.Zorluk=="ORTA"){
                    for (int i = 0; i < diziBoyut; i++)
                    {
                        if(ortaS<=kombinasyonlar[i,1]){
                          
                             _context.AossOnlineSinavSorular.Add(aossOnlineSinavSorular);
                             await _context.SaveChangesAsync();

                            return CreatedAtAction("GetAossOnlineSinavSorular", new { id = aossOnlineSinavSorular.Id }, aossOnlineSinavSorular);
                        }
                    }
                    throw new ArgumentException("ORTA Soru Ekleme Hakkınız Bitti");
                }
                else if(eklenmekIstenen.Zorluk=="ZOR"){
                    for (int i = 0; i < diziBoyut; i++)
                    {
                        if(zorS<=kombinasyonlar[i,2]){
                          
                             _context.AossOnlineSinavSorular.Add(aossOnlineSinavSorular);
                             await _context.SaveChangesAsync();

                            return CreatedAtAction("GetAossOnlineSinavSorular", new { id = aossOnlineSinavSorular.Id }, aossOnlineSinavSorular);
                        }
                    }
                    throw new ArgumentException("Zor Soru Ekleme Hakkınız Bitti ");
                }
              throw new ArgumentException("Hata");
            }
            else
            {
                 throw new ArgumentException("Bu soruyu ekleyemezsiniz. 100 puan aşılamaz.");
            }

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
