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
    public class AossOnlineSinavController : ControllerBase
    {
        private readonly AossContext _context;

        public AossOnlineSinavController(AossContext context)
        {
            _context = context;
        }
         /// <summary>
        ///Sınav listeleme
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossOnlineSinav
        /// </remarks>
        /// <response code="201">Sınavların listesi json olarak döner</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<IEnumerable<AossOnlineSinav>>> GetAossOnlineSinav()
        {
            return await _context.AossOnlineSinav.ToListAsync();
        }
        /// <summary>
        ///İdsi girilen sınavın bilgisi gelir
       /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// https://localhost:5001/api/AossOnlineSinav/1
        /// </remarks>
        /// <param name="id"> id parametresi sınavın idsidir. </param>
        /// <response code="201">Seçili sınav bilgileri json olarak döner</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<AossOnlineSinav>> GetAossOnlineSinav(long id)
        {
            var aossOnlineSinav = await _context.AossOnlineSinav.FindAsync(id);

            if (aossOnlineSinav == null)
            {
                return NotFound();
            }

            return aossOnlineSinav;
        }

        /// <summary>
        /// Sınav güncelleme işlemi
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// isteğin url kısmı:
        /// 
        /// https://localhost:5001//api/AossOnlineSinav/1
        /// 
        /// isteğin bodykısmı:
        /// 
        ///     PUT 
        ///     {
        ///         "id": 1,    
        ///        "baslama": "02.08.2020 18:15",
        ///         "bitis": "02.08.2020 19:15",
        ///         "sure": "60",
        ///         "alan": "Matematik"
        ///     }
        ///
        /// </remarks>
        /// <param name="aossOnlineSinav"> Online sınav verileri. Json formatında</param>
        /// <param name="id"> id parametresi sınavın idsidir.</param>
        /// <response code="201">Güncellenen sınavın bilgileri json formatında döner</response>

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<IActionResult> PutAossOnlineSinav(long id, AossOnlineSinav aossOnlineSinav)
        {
            if (id != aossOnlineSinav.Id)
            {
                return BadRequest();
            }

            _context.Entry(aossOnlineSinav).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AossOnlineSinavExists(id))
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
        /// Sınav Ekleme işlemi
        /// </summary>
        /// <remarks>
        /// Örnek istek:
        /// 
        /// isteğin url kısmı:
        /// 
        /// https://localhost:5001//api/AossOnlineSinav
        /// 
        /// isteğin bodykısmı:
        /// 
        ///     POST 
        ///     {
        ///        "baslama": "02.08.2020 18:15",
        ///         "bitis": "02.08.2020 19:15",
        ///         "sure": "60",
        ///         "alan": "Matematik"
        ///     }
        ///
        /// </remarks>
        /// <param name="aossOnlineSinav"> Online sınav verileri. Json formatında</param>
        /// <response code="201">Eklenen sınavın bilgileri json formatında döner</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<AossOnlineSinav>> PostAossOnlineSinav(AossOnlineSinav aossOnlineSinav)
        {
            _context.AossOnlineSinav.Add(aossOnlineSinav);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAossOnlineSinav", new { id = aossOnlineSinav.Id }, aossOnlineSinav);
        }

        // DELETE: api/AossOnlineSinav/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AossOnlineSinav>> DeleteAossOnlineSinav(long id)
        {
            var aossOnlineSinav = await _context.AossOnlineSinav.FindAsync(id);
            if (aossOnlineSinav == null)
            {
                return NotFound();
            }

            _context.AossOnlineSinav.Remove(aossOnlineSinav);
            await _context.SaveChangesAsync();

            return aossOnlineSinav;
        }

        private bool AossOnlineSinavExists(long id)
        {
            return _context.AossOnlineSinav.Any(e => e.Id == id);
        }
    }
}
