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

        // GET: api/AossSorular
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AossSorular>>> GetAossSorular()
        {
            return await _context.AossSorular.ToListAsync();
        }

        // GET: api/AossSorular/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AossSorular>> GetAossSorular(long id)
        {
            var aossSorular = await _context.AossSorular.FindAsync(id);

            if (aossSorular == null)
            {
                return NotFound();
            }

            return aossSorular;
        }
        [HttpGet("hataliSoru/{soruId}/{hocaId}")]
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
        // PUT: api/AossSorular/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
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

        // POST: api/AossSorular
        [HttpPost]
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
