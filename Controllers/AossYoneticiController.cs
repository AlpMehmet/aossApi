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

         /// <summary>
        ///Yönetici listeleme
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossYonetici
        /// </remarks>
        /// <response code="201">Yöneticilerin listesi json olarak döner</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<IEnumerable<AossYonetici>>> GetAossYonetici()
        {
            return await _context.AossYonetici.ToListAsync();
        }

        /// <summary>
        ///İdsi girilen yöneticinin  bilgisi gelir
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossYonetici/1
        /// </remarks>
        /// <param name="id"> id parametresi yöneticinin idsidir. </param>
        /// <response code="201">Seçili yönetici json olarak döner</response>
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<AossYonetici>> GetAossYonetici(long id)
        {
            var aossYonetici = await _context.AossYonetici.FindAsync(id);

            if (aossYonetici == null)
            {
                return NotFound();
            }

            return aossYonetici;
        }
        /// <summary>
        ///Yöneticinin sisteme girişi içindir. Mail ve şifre girilmelidir.
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossYonetici/örnek@örnek.com/123123
        /// </remarks>
        /// <param name="Mail"> Mail parametresi yöneticinin mail adresidir. </param>
        /// <param name="Sifre"> Şifre parametresi yöneticinin şifresidir.  </param>
        /// <response code="201">Eğer ki giriş başarılı ise yöneticinin id verisi döner başarısız ise -1 döner.</response>  
        [HttpGet("{Mail}/{Sifre}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
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
        /// <summary>
        /// Yöneticinin güncelleme işlemi
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// isteğin url kısmı:
        /// 
        /// https://localhost:5001/api/AossYonetici/1
        /// 
        /// isteğin bodykısmı:
        /// 
        ///     PUT 
        ///     {
        ///         "id": 1,    
        ///        "mail": "örnek@örnek.com",
        ///         "sifre": "21313sadasdsvc213",
        ///     }
        ///
        /// </remarks>
        /// <param name="id"> id parametresi yöneticinin idsidir. </param>
        /// <param name="aossYonetici"> Güncellenecek bilgiler json formatında olmalı</param>
        /// <response code="201">Güncellenen yöneticinin bilgileri json formatında döner</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
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

        /// <summary>
        /// Yönetici ekleme işlemi
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// isteğin url kısmı:
        /// 
        /// https://localhost:5001/api/AossYonetici
        /// 
        /// isteğin bodykısmı:
        /// 
        ///     POST 
        ///     {
        ///        "mail": "örnekY@örnek.com",
        ///         "sifre": "yö123",
        ///     }
        ///
        /// </remarks>
        /// <param name="aossYonetici"> Eklenecek bilgiler json formatında olmalı</param>
        /// <response code="201">Eklenen yöneticinin bilgileri json formatında döner</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<AossYonetici>> PostAossYonetici( AossYonetici aossYonetici)
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
