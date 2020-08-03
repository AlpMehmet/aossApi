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
    public class AossHocaController : ControllerBase
    {
        private readonly AossContext _context;

        public AossHocaController(AossContext context)
        {
            _context = context;
        }

         /// <summary>
        ///Hoca listeleme
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossHoca
        /// </remarks>
        /// <response code="201">Hocaların listesi json olarak döner</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<IEnumerable<AossHoca>>> GetAossHoca()
        {
            return await _context.AossHoca.ToListAsync();
        }

        /// <summary>
        ///İdsi girilen hocanın bilgisi gelir
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossHoca/1
        /// </remarks>
        /// <param name="id"> id parametresi hocanın idsidir. </param>
        /// <response code="201">Seçili hoca json olarak döner</response>
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<AossHoca>> GetAossHoca(long id)
        {
            var aossHoca = await _context.AossHoca.FindAsync(id);

            if (aossHoca == null)
            {
                return NotFound();
            }

            return aossHoca;
        }
        /// <summary>
        ///Hocanın sisteme girişi içindir. Mail ve şifre girilmelidir.
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossHoca/örnek@örnek.com/123123
        /// </remarks>
        /// <param name="mail"> Mail parametresi hocanın mail adresidir. </param>
        /// <param name="Sifre"> Şifre parametresi hocanın şifresidir.  </param>
        /// <response code="201">Eğer ki giriş başarılı ise hocanın id verisi döner başarısız ise -1 döner.</response>  
        [HttpGet("{mail}/{Sifre}")]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public long Giris(string mail, string Sifre)
        {
            AossHoca a = _context.AossHoca.Where(x => x.Mail == mail && x.Sifre == Sifre).FirstOrDefault();
            if(a==null){
                return -1;
            }
            else{
                return a.Id;
            }
        }   

        /// <summary>
        /// Hoca güncelleme işlemi
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// isteğin url kısmı:
        /// 
        /// https://localhost:5001/api/AossHoca/1
        /// 
        /// isteğin bodykısmı:
        /// 
        ///     PUT 
        ///     {
        ///         "id": 1,    
        ///        "mail": "örnek@örnek.com",
        ///         "sifre": "21313sadasdsvc213",
        ///         "adSoyad": "Örnek ör",
        ///         "alani": "Matematik"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"> id parametresi hocanın idsidir. </param>
        /// <param name="aossHoca"> Güncellenecek bilgiler json formatında olmalı</param>
        /// <response code="201">Güncellenen hocanın bilgileri json formatında döner</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PutAossHoca(long id, AossHoca aossHoca)
        {
            if (id != aossHoca.Id)
            {
                return BadRequest();
            }

            _context.Entry(aossHoca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossHocaExists(id))
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
        /// Hoca ekleme işlemi
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
        ///         "mail": "örnek@örnek.com",
        ///         "sifre": "21313sadasdsvc213",
        ///         "adSoyad": "Örnek ör",
        ///         "alani": "Matematik"
        ///     }
        ///
        /// </remarks>
        /// <param name="aossHoca"> Eklenecek bilgiler json formatında olmalı</param>
        /// <response code="201">Eklenen hocanın bilgileri json formatında döner</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<AossHoca>> PostAossHoca(AossHoca aossHoca)
        {
            _context.AossHoca.Add(aossHoca);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAossHoca", new { id = aossHoca.Id }, aossHoca);
        }

        // DELETE: api/AossHoca/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AossHoca>> DeleteAossHoca(long id)
        {
            var aossHoca = await _context.AossHoca.FindAsync(id);
            if (aossHoca == null)
            {
                return NotFound();
            }

            _context.AossHoca.Remove(aossHoca);
            await _context.SaveChangesAsync();

            return aossHoca;
        }

        private bool AossHocaExists(long id)
        {
            return _context.AossHoca.Any(e => e.Id == id);
        }
    }
}
