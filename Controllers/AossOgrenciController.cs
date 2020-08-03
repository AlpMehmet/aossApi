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

         /// <summary>
        ///Öğrenci listeleme
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossOgrenci
        /// </remarks>
        /// <response code="201">Öğrencilerin listesi json olarak döner</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<IEnumerable<AossOgrenci>>> GetAossOgrenci()
        {
            return await _context.AossOgrenci.ToListAsync();
        }

        /// <summary>
        ///İdsi girilen öğrencinin bilgisi gelir
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossOgrenci/1
        /// </remarks>
        /// <param name="id"> id parametresi öğrencinin idsidir. </param>
        /// <response code="201">Seçili öğrencinin json olarak döner</response>
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<AossOgrenci>> GetAossOgrenci(long id)
        {
            var aossOgrenci = await _context.AossOgrenci.FindAsync(id);

            if (aossOgrenci == null)
            {
                return NotFound();
            }

            return aossOgrenci;
        }
        /// <summary>
        ///Öğrencinin sisteme girişi içindir. Öğrenci numarası ve şifre girilmelidir.
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossOgrenci/213132113321/123123
        /// </remarks>
        /// <param name="OgrenciNo"> Mail parametresi öğrencinin öğrenci numarasıdır.</param>
        /// <param name="Sifre"> Şifre parametresi öğrencinin şifresidir.  </param>
        /// <response code="201">Eğer ki giriş başarılı ise öğrencinin int olarak id verisi döner başarısız ise int olarak -1 döner.</response>  
        [HttpGet("{OgrenciNo}/{Sifre}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
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
        /// <summary>
        ///Öğrencinin sinav ekranında soru işaretlemesi içindir. 
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossOgrenci/Sinav/Cevap/1/A
        /// </remarks>
        /// <param name="OnlineSinavOgrenciSorularId"> OnlineSinavOgrenciSorularId öğrencinin cevapladığı sorunun(randomize edilmiş sorunun) idsidir.</param>
        /// <param name="isaretli"> Öğrencinin işaretlediği cevap şıkkıdır.A,B,C,D veya E  </param>
        /// <response code="201">Eğer ekleme başarılı olursa string olarak "Ekleme Sorunsuz Yapıldı" döner. Daha önceden eklenmiş ise string olarak "Soru Daha Önceden Kayıt Edilmiş" döner.</response>      
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
        /// <summary>
        ///Sınavı bitirmek için çalıştırılmalıdır. 
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossOgrenci/Sinav/Bitir/11
        /// </remarks>
        /// <param name="OnlineSinavOgrenciId">OnlineSinavOgrenciId verisi girilmelidir. Sınav oluşturulurken bu veri tutulabilir.</param>
        /// <response code="201">Girilen veride yanlışlık yoksa hocanın girdiği kriterlere göre öğrecinin puanı string olarak döndürülür.</response>  
        [HttpPut("/Sinav/Bitir/{OnlineSinavOgrenciId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]

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
                else if(soru.IsaretlenenCevap!=soru.DogruCevap)
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

         
        
         
        /// <summary>
        /// Öğrenci güncelleme işlemi
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// isteğin url kısmı:
        /// 
        /// https://localhost:5001/api/AossOgrenci/1
        /// 
        /// isteğin bodykısmı:
        /// 
        ///     PUT 
        ///     {
        ///         "id": 1,    
        ///        "öğrenciNo": "2131313123",
        ///         "sifre": "2131sadsdasd",
        ///         "adSoyad": "Örnek ör2",
        ///         "alani": "Matematik"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"> id parametresi öğrencinin idsidir. </param>
        /// <param name="aossOgrenci"> Güncellenecek bilgiler json formatında olmalı</param>
        /// <response code="201">Güncellenen öğrencinin bilgileri json formatında döner</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
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

        /// <summary>
        /// Öğrenci ekleme işlemi
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// isteğin url kısmı:
        /// 
        /// https://localhost:5001/api/AossHoca
        /// 
        /// isteğin bodykısmı:
        /// 
        ///     POST 
        ///     {
        ///         "ogrenciNo": "21313213131",
        ///         "sifre": "21313sadasdsvc213",
        ///         "adSoyad": "Örnek ör2",
        ///         "alani": "Matematik"
        ///     }
        ///
        /// </remarks>
        /// <param name="aossOgrenci"> Eklenecek bilgiler json formatında olmalı</param>
        /// <response code="201">Eklenen öğrencinin bilgileri json formatında döner</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
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
