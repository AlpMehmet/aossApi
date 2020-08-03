using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AossAPI.Models;
using AossApi.Models;
using System.Net.Mail;
namespace AossAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AossSorularController : ControllerBase
    {
     
        private readonly AossContext _context;

        public AossSorularController(AossContext context)
        {
            _context = context;
        }
         /// <summary>
        ///Alana göre Soru listeleme
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossSorular/Matematik
        /// </remarks>
        /// <param name="alan">Sorunun alanı</param>
        /// <response code="201">Soruların listesi json olarak döner</response>
        [HttpGet("alan")]
        public async Task<ActionResult<IEnumerable<AossSorular>>> GetAossSorular(string alan)
        {
            return await _context.AossSorular.Where(x=>x.SoruAlani==alan).ToListAsync();
        }

        /// <summary>
        ///Bir soru hatalı olarak işaretlenmek istediğinde burası çalışmalı. Burada soruyu hatalı olarak belirten hocanın alanına göre o alandaki hocaları mail gider ve gerekli tabloya bunun verisi eklenir.
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossSorular/hataliSoru/1/3
        /// </remarks>
        /// <param name="soruId">Hatalı işaretlenmek istenen sorunun idsi girilir.</param>
        /// <param name="hocaId">Soruyu hatalı işaretleyen hocanın idsi gidilir.</param>
        /// <response code="201">Doğru bir şekilde mail yollama ve eklenme yapıldıysa string olarak "Mailler Yollandı Ve Soru Oylama İşlemine Kadar Hatalı Olarak İşaretlendi" mesajı döner eğer hata ile karşılaşılırsa string olarak hata mesajı döndürülür.</response>  
        [HttpGet("hataliSoru/{soruId}/{hocaId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]        

        public string hataliSoru(long soruId, long hocaId)
        {
            MailMessage mesaj = new MailMessage();
            SmtpClient istemci = new SmtpClient();
            istemci.Credentials = new System.Net.NetworkCredential("aossstajmail@gmail.com","1a2O3s4S.");
            istemci.Port=587;
            istemci.Host="smtp.gmail.com";
            istemci.EnableSsl = true;
            
            AossSorular soru = _context.AossSorular.Where(x => x.Id == soruId).FirstOrDefault();
            if(soru==null){
                return "Hata: Yollanan Soru Idsi Mevcut Değil";
            }
            else{
                 List<AossHoca>  hocalar = _context.AossHoca.Where(x => x.Alani == soru.SoruAlani).ToList();
                 if(hocalar==null){
                    return "Hata: Yollanan Hoca Idsi Mevcut Değil";
                 }
                 else
                 {
                     try
                     {
                          foreach (var hoca in hocalar)
                            {
                                mesaj.To.Add(hoca.Mail); 
                            }
                            mesaj.From=new MailAddress("aossstajmail@gmail.com");
                            mesaj.Subject="Hatalı Soru";
                            mesaj.Body="Hatalı İşaretlenen Sorunun İdsi:"+soru.Id+" Soruyu Hatalı Olarak Belirten Hoca: "+hocaId+Environment.NewLine+"Lütfen sisteme girip sorunun hatalı olup olmamasını belirtiniz.";
                            istemci.Send(mesaj);  
                             AossHataliSorular aossHataliSorular  = new AossHataliSorular();
                             aossHataliSorular.SoruId=soru.Id;
                             aossHataliSorular.Alani=soru.SoruAlani;
                             _context.AossHataliSorular.Add(aossHataliSorular);
                            _context.SaveChangesAsync();   
                            return "Mailler Yollandı Ve Soru Oylama İşlemine Kadar Hatalı Olarak İşaretlendi";      
                     }
                     catch (System.Exception)
                     {
                         
                         return "Mail Yollanırken Hata İle Karşılaşıldı";      
                     }
                   
                 }
            }
        }  
        /// <summary>
        /// Soru güncelleme işlemi 
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// isteğin url kısmı:
        /// 
        /// https://localhost:5001/api/AossSorular/1
        /// 
        /// isteğin bodykısmı:
        /// 
        ///     PUT 
        ///     {
        /// "id": 0,
        ///  "soru": "soru?",
        /// "dogruCevap": "doğrucevap",
        /// "cevap1": "cevap1",
        /// "cevap2": "cevap2",
        /// "cevap3": "cevap3",
        /// "cevap4": "cevap4",
        /// "soruAlani": "Matematik",
        /// "zorluk": "ZOR"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"> id parametresi sorunun idsidir. </param>
        /// <param name="aossSorular"> Güncellenecek bilgiler json formatında olmalı</param>
        /// <response code="201">Güncellenen sorunun bilgileri json formatında döner</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PutAossSorular(long id, AossSorular aossSorular)
        {
            if (id != aossSorular.Id)
            {
                return BadRequest();
            }

            _context.Entry(aossSorular).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossSorularExists(id))
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
        /// Soru ekleme işlemi
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// isteğin url kısmı:
        /// 
        /// https://localhost:5001/api/AossSorular
        /// 
        /// isteğin bodykısmı:
        /// 
        ///     POST 
        ///     {
        ///     "soru": "soru?",
        ///     "dogruCevap": "doğrucevap",
        ///     "cevap1": "cevap1",
        ///     "cevap2": "cevap2",
        ///     "cevap3": "cevap3",
        ///     "cevap4": "cevap4",
        ///     "soruAlani": "Matematik",
        ///     "zorluk": "ZOR"
        ///     }
        ///
        /// </remarks>
        /// <param name="aossSorular"> Eklenecek bilgiler json formatında olmalı</param>
        /// <response code="201">Eklenen hocanın bilgileri json formatında döner</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<AossSorular>> PostAossSorular(AossSorular aossSorular)
        {
            _context.AossSorular.Add(aossSorular);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAossSorular), new { id = aossSorular.Id }, aossSorular);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AossSorular>> DeleteAossSorular(long id)
        {
            var aossSorular = await _context.AossSorular.FindAsync(id);
            if (aossSorular == null)
            {
                return NotFound();
            }

            _context.AossSorular.Remove(aossSorular);
            await _context.SaveChangesAsync();

            return aossSorular;
        }

        private bool AossSorularExists(long id)
        {
            return _context.AossSorular.Any(e => e.Id == id);
        }
    }
}
