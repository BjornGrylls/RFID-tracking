using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFID_tracking_API.Data;

namespace RFID_tracking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliceController : ControllerBase
    {
        private readonly RFIDTrackingDbContext _context;

        public PoliceController(RFIDTrackingDbContext context)
        {
            _context = context;
        }

        // GET: api/Police
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Police>>> GetPolice()
        {
            return await _context.Police.ToListAsync();
        }

        // GET: api/Police/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Police>> GetPolice(Guid id)
        {
            var police = await _context.Police.FindAsync(id);

            if (police == null)
            {
                return NotFound();
            }

            return police;
        }

        // PUT: api/Police/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolice(Guid id, Police police)
        {
            if (id != police.Id)
            {
                return BadRequest();
            }

            _context.Entry(police).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoliceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Police
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Police>> PostPolice(Police police)
        {
            _context.Police.Add(police);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPolice", new { id = police.Id }, police);
        }

        // DELETE: api/Police/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolice(Guid id)
        {
            var police = await _context.Police.FindAsync(id);
            if (police == null)
            {
                return NotFound();
            }

            _context.Police.Remove(police);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool PoliceExists(Guid id)
        {
            return _context.Police.Any(e => e.Id == id);
        }
    }
}
