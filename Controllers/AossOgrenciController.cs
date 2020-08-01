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
        [HttpPut("/Sinav/Cevap/{OnlineSinavOgrenciSorularId}/{isaretli}")]
        public string soruIs(int OnlineSinavOgrenciSorularId, string isaretli)
        {
            AossOnlineSinavOgrenciSorular iS = _context.AossOnlineSinavOgrenciSorular.Where(x => x.Id == OnlineSinavOgrenciSorularId ).FirstOrDefault();
            if(iS.IsaretlenenCevap==null){
            AossOnlineSinavOgrenciSorular guncelS = new AossOnlineSinavOgrenciSorular();
            guncelS.Id=iS.Id;
            guncelS.OnlineSinavOgrenciId=iS.OnlineSinavOgrenciId;
            guncelS.Soru=iS.Soru;
            guncelS.SoruId=iS.SoruId;
            guncelS.A=iS.A;
            guncelS.B=iS.B;
            guncelS.C=iS.C;
            guncelS.D=iS.D;
            guncelS.E=iS.E;
            guncelS.DogruCevap=iS.DogruCevap;
            guncelS.IsaretlenenCevap=isaretli;
            _context.Entry(guncelS).State = EntityState.Modified;

            _context.SaveChangesAsync();
            return "Ekleme Sorunsuz Yapıldı";
            }
            else
            {
               return "Soru Daha Önceden Kayıt Edilmiş";
            }
         
        }
        [HttpPut("/Sinav/Bitir/{OnlineSinavOgrenciId}")]
        public string soruIs(int OnlineSinavOgrenciId)
        {
            int dogruS=0,yanlisS=0,bosS=0;float puan=0;
            AossOnlineSinavOgrenci sinav = _context.AossOnlineSinavOgrenci.Where(x => x.Id == OnlineSinavOgrenciId).FirstOrDefault();
            List<AossOnlineSinavOgrenciSorular> sorular = _context.AossOnlineSinavOgrenciSorular.Where(x => x.OnlineSinavOgrenciId == sinav.Id).ToList();
            int kacSoru = _context.AossOnlineSinavSorular.Where(x => x.OnlineSinavId == sinav.OnlineSinavId).Count();
            AossZorlukPuanlama zorlukP = _context.AossZorlukPuanlama.Where(x => x.OnlineSinavId ==sinav.OnlineSinavId).FirstOrDefault();

            foreach (var soru in sorular)
            {
                if(soru.IsaretlenenCevap==soru.DogruCevap)
                {
                    AossSorular zorlukAl= _context.AossSorular.Where(x=> x.Id == soru.SoruId).FirstOrDefault();
                    if(zorlukAl.Zorluk=="ZOR")
                    {
                        puan+=zorlukP.ZorPuan;
                    }
                    else if(zorlukAl.Zorluk=="ORTA")
                    {
                        puan+=zorlukP.OrtaPuan;
                    }
                    else if(zorlukAl.Zorluk=="KOLAY"){
                        puan+=zorlukP.OrtaPuan;             
                    }
                    dogruS++;
                }
                else if(soru.IsaretlenenCevap!=soru.DogruCevap),
                {
                    yanlisS++;
                }
            }
            if(kacSoru!=dogruS+yanlisS)
            {
                bosS=Math.Abs(dogruS-yanlisS);
            }
            else
            {
                bosS=0;
            }
            AossOnlineSinavOgrenci kayit= new AossOnlineSinavOgrenci();
            kayit.Id=sinav.Id;
            kayit.OgId=sinav.OgId;
            kayit.BosS=bosS;
            kayit.Puan=Convert.ToInt32(puan);
            kayit.YanlisS=yanlisS;
            kayit.OnlineSinavId=sinav.OnlineSinavId;
            kayit.DogruS=sinav.OnlineSinavId;
            _context.Entry(kayit).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return puan.ToString();
        
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

            return CreatedAtAction(nameof(GetAossOgrenci), new { id = aossOgrenci.Id }, aossOgrenci);
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
