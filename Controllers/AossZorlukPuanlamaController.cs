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
    public class AossZorlukPuanlamaController : ControllerBase
    {
        private readonly AossContext _context;

        public AossZorlukPuanlamaController(AossContext context)
        {
            _context = context;
        }

         /// <summary>
        /// Verilerin listelenmesi
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossZorlukPuanlama
        /// </remarks>
        /// <response code="201">Tablodaki verilerin listesi json olarak döner</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<IEnumerable<AossZorlukPuanlama>>> GetAossZorlukPuanlama()
        {
            return await _context.AossZorlukPuanlama.ToListAsync();
        }

        /// <summary>
        ///İdsi girilen verinin bilgisi gelir
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossZorlukPuanlama/1
        /// </remarks>
        /// <param name="id"> id parametresi </param>
        /// <response code="201">Seçili veri json olarak döner</response>
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<AossZorlukPuanlama>> GetAossZorlukPuanlama(long id)
        {
            var aossZorlukPuanlama = await _context.AossZorlukPuanlama.FindAsync(id);

            if (aossZorlukPuanlama == null)
            {
                return NotFound();
            }

            return aossZorlukPuanlama;
        }

        /// <summary>
        /// Verinin güncelleme işlemi
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// isteğin url kısmı:
        /// 
        /// https://localhost:5001/api/AossZorlukPuanlama/1
        /// 
        /// isteğin bodykısmı:
        /// 
        ///     PUT 
        ///   {
        ///     "id": 1,
        ///     "kolayPuan": 10,
        ///     "ortaPuan": 10,
        ///     "zorPuan": 10,
        ///     "onlineSinavId": 3
        ///   }
        ///
        /// </remarks>
        /// <param name="id"> id parametresi verinin idsidir. </param>
        /// <param name="aossZorlukPuanlama"> Güncellenecek bilgiler json formatında olmalı</param>
        /// <response code="201">Güncellenen bilgiler json formatında döner</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PutAossZorlukPuanlama(long id, AossZorlukPuanlama aossZorlukPuanlama)
        {
            if (id != aossZorlukPuanlama.Id)
            {
                return BadRequest();
            }

            _context.Entry(aossZorlukPuanlama).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossZorlukPuanlamaExists(id))
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
        /// Veri ekleme işlemi. Sınav için hocanın girmesi gereken puanlama kısmı bu kısım ikinci olarak onlinesınav verileri girildikten sonra girilmeli  
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// isteğin url kısmı:
        /// 
        /// https://localhost:5001/api/AossZorlukPuanlama
        /// 
        /// isteğin bodykısmı:
        /// 
        ///     POST 
        ///   {
        ///     "kolayPuan": 10,
        ///     "ortaPuan": 10,
        ///     "zorPuan": 10,
        ///     "onlineSinavId": 3
        ///   }
        ///
        /// </remarks>
        /// <param name="aossZorlukPuanlama"> Eklenecek bilgiler json formatında olmalı</param>
        /// <response code="201">Eklenen verinin bilgileri json formatında döner</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<AossZorlukPuanlama>> PostAossZorlukPuanlama(AossZorlukPuanlama aossZorlukPuanlama)
        {
            _context.AossZorlukPuanlama.Add(aossZorlukPuanlama);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAossZorlukPuanlama", new { id = aossZorlukPuanlama.Id }, aossZorlukPuanlama);
        }

        // DELETE: api/AossZorlukPuanlama/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AossZorlukPuanlama>> DeleteAossZorlukPuanlama(long id)
        {
            var aossZorlukPuanlama = await _context.AossZorlukPuanlama.FindAsync(id);
            if (aossZorlukPuanlama == null)
            {
                return NotFound();
            }

            _context.AossZorlukPuanlama.Remove(aossZorlukPuanlama);
            await _context.SaveChangesAsync();

            return aossZorlukPuanlama;
        }

        private bool AossZorlukPuanlamaExists(long id)
        {
            return _context.AossZorlukPuanlama.Any(e => e.Id == id);
        }
    }
}
