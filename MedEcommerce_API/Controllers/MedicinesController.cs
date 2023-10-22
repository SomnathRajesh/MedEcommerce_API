using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedEcommerce_DB;

namespace MedEcommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Medicines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetMedicines()
        {
          if (_context.Medicines == null)
          {
              return NotFound();
          }
            return await _context.Medicines.Include(c=>c.Category).ToListAsync();
        }

        // GET: api/Medicines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicine>> GetMedicine(int id)
        {
          if (_context.Medicines == null)
          {
              return NotFound();
          }
            var medicine = await _context.Medicines.Include(c => c.Category).Where(m=>m.Id==id).FirstOrDefaultAsync();

            if (medicine == null)
            {
                return NotFound();
            }

            return medicine;
        }

        // PUT: api/Medicines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Medicine>> PutMedicine(int id, Medicine medicine)
        {
            if (id != medicine.Id)
            {
                return BadRequest();
            }

            _context.Entry(medicine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var med = await _context.Medicines.Include(c => c.Category).Where(m => m.Id == id).FirstOrDefaultAsync();
            if (med == null)
            {
                return NotFound();
            }

            return med;
        }

        // POST: api/Medicines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Medicine>> PostMedicine(Medicine medicine)
        {
          if (_context.Medicines == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Medicines'  is null.");
          }
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();
            var med = await _context.Medicines.Include(c => c.Category).Where(m => m.Name == medicine.Name).FirstOrDefaultAsync();

            return CreatedAtAction("GetMedicine", new { id = medicine.Id }, med);
        }

        // DELETE: api/Medicines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            if (_context.Medicines == null)
            {
                return NotFound();
            }
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }

            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicineExists(int id)
        {
            return (_context.Medicines?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
