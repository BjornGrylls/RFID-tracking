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
    public class ShootersController : ControllerBase
    {
        private readonly RFIDTrackingDbContext _context;

        public ShootersController(RFIDTrackingDbContext context)
        {
            _context = context;
        }

        // GET: api/Shooters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shooter>>> GetShooters()
        {
            return await _context.Shooters.ToListAsync();
        }

        // GET: api/Shooters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shooter>> GetShooter(Guid id)
        {
            var shooter = await _context.Shooters.FindAsync(id);

            if (shooter == null)
            {
                return NotFound();
            }

            return shooter;
        }

        // PUT: api/Shooters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShooter(Guid id, Shooter shooter)
        {
            if (id != shooter.Id)
            {
                return BadRequest();
            }

            _context.Entry(shooter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShooterExists(id))
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

        // POST: api/Shooters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shooter>> PostShooter(Shooter shooter)
        {
            _context.Shooters.Add(shooter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShooter", new { id = shooter.Id }, shooter);
        }

        // DELETE: api/Shooters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShooter(Guid id)
        {
            var shooter = await _context.Shooters.FindAsync(id);
            if (shooter == null)
            {
                return NotFound();
            }

            _context.Shooters.Remove(shooter);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ShooterExists(Guid id)
        {
            return _context.Shooters.Any(e => e.Id == id);
        }
    }
}
