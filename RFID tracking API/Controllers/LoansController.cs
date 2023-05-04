using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFID_tracking_API.Data;

namespace RFID_tracking_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase {
        private readonly RFIDTrackingDbContext _context;

        public LoansController(RFIDTrackingDbContext context) {
            _context = context;
        }

        // GET: api/Loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans() {
            return await _context.Loans.ToListAsync();
        }

        // GET: api/Loans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(Guid id) {
            var loan = await _context.Loans.FindAsync(id);

            if (loan == null) {
                return NotFound();
            }

            return loan;
        }

        // PUT: api/Loans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoan(Guid id, Loan loan) {
            if (id != loan.Id) {
                return BadRequest();
            }

            _context.Entry(loan).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!LoanExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Loans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(Loan loan) {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoan", new { id = loan.Id }, loan);
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(Guid id) {
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null) {
                return NotFound();
            }

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool LoanExists(Guid id) {
            return _context.Loans.Any(e => e.Id == id);
        }
    }
}
