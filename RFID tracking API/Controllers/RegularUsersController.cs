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
    public class RegularUsersController : ControllerBase
    {
        private readonly RFIDTrackingDbContext _context;

        public RegularUsersController(RFIDTrackingDbContext context)
        {
            _context = context;
        }

        // GET: api/RegularUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegularUsers>>> GetRegularUsers()
        {
            return await _context.RegularUsers.ToListAsync();
        }

        // GET: api/RegularUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegularUsers>> GetRegularUsers(Guid id)
        {
            var regularUsers = await _context.RegularUsers.FindAsync(id);

            if (regularUsers == null)
            {
                return NotFound();
            }

            return regularUsers;
        }

        // PUT: api/RegularUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegularUsers(Guid id, RegularUsers regularUsers)
        {
            if (id != regularUsers.Id)
            {
                return BadRequest();
            }

            _context.Entry(regularUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegularUsersExists(id))
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

        // POST: api/RegularUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RegularUsers>> PostRegularUsers(RegularUsers regularUsers)
        {
            _context.RegularUsers.Add(regularUsers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegularUsers", new { id = regularUsers.Id }, regularUsers);
        }

        // DELETE: api/RegularUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegularUsers(Guid id)
        {
            var regularUsers = await _context.RegularUsers.FindAsync(id);
            if (regularUsers == null)
            {
                return NotFound();
            }

            _context.RegularUsers.Remove(regularUsers);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool RegularUsersExists(Guid id)
        {
            return _context.RegularUsers.Any(e => e.Id == id);
        }
    }
}
