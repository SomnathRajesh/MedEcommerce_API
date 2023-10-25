using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedEcommerce_DB;
using MedEcommerce_Core;

namespace MedEcommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineService  _medicineService;

        public MedicinesController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        // GET: api/Medicines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetMedicines()
        {
          var medicines = await _medicineService.GetMedicinesAsync();
            return Ok(medicines);
        }

        // GET: api/Medicines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicine>> GetMedicine(int id)
        {
          var medicine = await _medicineService.GetMedicineByIdAsync(id);
            if(medicine == null)
            {
                return NotFound();
            }
            return Ok(medicine);
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

            var updatedMed = await _medicineService.UpdateMedicineAsync(id, medicine);
            if (updatedMed == null)
            {
                return NotFound();
            }

            return Ok(updatedMed);
        }

        // POST: api/Medicines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Medicine>> PostMedicine(Medicine medicine)
        {
            var med = await _medicineService.CreateMedicineAsync(medicine);

            return CreatedAtAction("GetMedicine", new { id = med.Id }, med);
        }

        // DELETE: api/Medicines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var result = await _medicineService.DeleteMedicineAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        
    }
}
