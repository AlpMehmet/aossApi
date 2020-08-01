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
         
        [HttpGet("KaydiBitir/{OnlineSinavId}")]
        public long Giris(int OnlineSinavId)
        {//AossOnlineSinavOgrenci
            AossOnlineSinav sinav = _context.AossOnlineSinav.Where(x => x.Id == OnlineSinavId).FirstOrDefault();
            if(sinav==null){
                return -1;
            }
            else{
               List<AossOgrenci> ogrenciler = _context.AossOgrenci.Where(x => x.Alani == sinav.Alan).ToList();
               foreach (var ogrenci in ogrenciler)
               {
                AossOnlineSinavOgrenci aossOnlineSinavOgrenci = new AossOnlineSinavOgrenci();  
                aossOnlineSinavOgrenci.OgId=Convert.ToInt32(ogrenci.Id);
                aossOnlineSinavOgrenci.OnlineSinavId=OnlineSinavId;
                _context.AossOnlineSinavOgrenci.Add(aossOnlineSinavOgrenci);
                 _context.SaveChangesAsync();
                
                List<AossOnlineSinavSorular> sorular = _context.AossOnlineSinavSorular.Where(x => x.OnlineSinavId == OnlineSinavId).ToList();
                foreach (var soru in sorular)
                {
                    AossSorular gelensoru = _context.AossSorular.Where(x => x.Id == soru.SoruId).FirstOrDefault();
                    Random rastgele = new Random();
                    int[] kontrol = new int[6];
                    int say=0;
                    bool durum=false;
                    while (true)
                    {    
                         int a = rastgele.Next(1,5);
                         for (int i = 0; i < say; i++)
                         {
                             if(kontrol[i]==a) durum=true;
                         }
                         if(!durum)
                         {
                             kontrol[say]=a;
                             say++;
                         }
                         if(say==4) break;
                    }
                    int b = rastgele.Next(1,5);
                    int dogru=0;
                    kontrol[5]=b;
                    AossOnlineSinavOgrenciSorular ogSoru = new AossOnlineSinavOgrenciSorular();
                    for (int i = 0; i < 6; i=i+2)
                    {
                        if(dogru==kontrol[5])
                        {
                            
                        }
                        if (kontrol[i]==1)
                        {
                            if (kontrol[i+1]==2)
                            {
                                dogru=2;
                                ogSoru.A=gelensoru.Cevap1;
                                ogSoru.B=gelensoru.DogruCevap;
                                ogSoru.DogruCevap="B";
                            }
                            else if (kontrol[i+1]==3)
                            {
                                dogru=3;
                                ogSoru.A=gelensoru.Cevap2;
                                ogSoru.C=gelensoru.DogruCevap;
                                ogSoru.DogruCevap="C";
                            }
                            else if (kontrol[i+1]==4)
                            {   
                                dogru=4;
                                ogSoru.A=gelensoru.Cevap3;
                                ogSoru.D=gelensoru.DogruCevap;
                                ogSoru.DogruCevap="D";
                            }
                            else if (kontrol[i+1]==5)
                            {   
                                dogru=5;
                                ogSoru.A=gelensoru.Cevap4;
                                ogSoru.E=gelensoru.DogruCevap;
                                ogSoru.DogruCevap="E";
                            }
                        }
                        else if (kontrol[i]==2)
                        {
                            if (kontrol[i+1]==1)
                            {
                                dogru=2;
                                ogSoru.B=gelensoru.DogruCevap;
                                ogSoru.A=gelensoru.Cevap1;
                                ogSoru.DogruCevap="B";
                            }
                            else if (kontrol[i+1]==3)
                            {
                                ogSoru.B=gelensoru.Cevap2;
                                ogSoru.C=gelensoru.Cevap1;
                            }
                            else if (kontrol[i+1]==4)
                            {
                                ogSoru.B=gelensoru.Cevap3;
                                ogSoru.D=gelensoru.Cevap1;
                            }
                            else if (kontrol[i+1]==5)
                            {
                                ogSoru.B=gelensoru.Cevap4;
                                ogSoru.E=gelensoru.Cevap1;
                            }
                        }
                        else if (kontrol[i]==3)
                        {
                            if (kontrol[i+1]==1)
                            {
                                dogru=3;
                                ogSoru.C=gelensoru.DogruCevap;
                                ogSoru.A=gelensoru.Cevap2;
                                ogSoru.DogruCevap="C";
                            }
                            else if (kontrol[i+1]==2)
                            {
                                ogSoru.C=gelensoru.Cevap1;
                                ogSoru.B=gelensoru.Cevap2;
                            }
                            else if (kontrol[i+1]==4)
                            {
                                ogSoru.C=gelensoru.Cevap3;
                                ogSoru.D=gelensoru.Cevap2;
                            }
                            else if (kontrol[i+1]==5)
                            {
                                ogSoru.C=gelensoru.Cevap4;
                                ogSoru.E=gelensoru.Cevap2;
                            }                            
                        }
                        else if (kontrol[i]==4)
                        {
                           
                            if (kontrol[i+1]==1)
                            {
                                dogru=4;
                                ogSoru.D=gelensoru.DogruCevap;
                                ogSoru.A=gelensoru.Cevap3;
                                ogSoru.DogruCevap="D";
                            }
                            else if (kontrol[i+1]==2)
                            {
                                ogSoru.D=gelensoru.Cevap1;
                                ogSoru.B=gelensoru.Cevap3;
                            }
                            else if (kontrol[i+1]==3)
                            {
                                ogSoru.D=gelensoru.Cevap2;
                                ogSoru.D=gelensoru.Cevap3;
                            }
                            else if (kontrol[i+1]==5)
                            {
                                ogSoru.D=gelensoru.Cevap4;
                                ogSoru.E=gelensoru.Cevap3;
                            }                               
                        }
                        else if (kontrol[i]==5)
                        {
                            
                            if (kontrol[i+1]==1)
                            {
                                dogru=5;
                                ogSoru.E=gelensoru.DogruCevap;
                                ogSoru.A=gelensoru.Cevap4;
                                ogSoru.DogruCevap="E";
                            }
                            else if (kontrol[i+1]==2)
                            {
                                ogSoru.E=gelensoru.Cevap1;
                                ogSoru.B=gelensoru.Cevap4;
                            }
                            else if (kontrol[i+1]==3)
                            {
                                ogSoru.E=gelensoru.Cevap2;
                                ogSoru.C=gelensoru.Cevap4;
                            }
                            else if (kontrol[i+1]==4)
                            {
                                ogSoru.E=gelensoru.Cevap3;
                                ogSoru.D=gelensoru.Cevap4;
                            }                         
                        }
                        if(i==4){
                            if(dogru==0) {
                                ogSoru.DogruCevap="A";
                                dogru=1;
                                }
                        }
                    }
                    AossOnlineSinavOgrenci eklenenOnlineSinavOg = _context.AossOnlineSinavOgrenci.Where(x => x.OgId == ogrenci.Id && x.OnlineSinavId == OnlineSinavId).FirstOrDefault();
                    ogSoru.OnlineSinavOgrenciId=eklenenOnlineSinavOg.Id;
                    ogSoru.Soru=gelensoru.Soru;
                    ogSoru.SoruId=gelensoru.Id;
                    _context.AossOnlineSinavOgrenciSorular.Add(ogSoru);
                    _context.SaveChangesAsync();
 
                }

               }
            }
            return 1;
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
