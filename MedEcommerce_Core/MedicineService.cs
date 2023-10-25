using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedEcommerce_DB;
using Microsoft.EntityFrameworkCore;

namespace MedEcommerce_Core
{
    public class MedicineService : IMedicineService
    {
        private readonly ApplicationDbContext _context;
        public MedicineService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Medicine> CreateMedicineAsync(Medicine medicine)
        {
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();
            var med = await _context.Medicines.Include(c => c.Category).Where(m => m.Name == medicine.Name).FirstOrDefaultAsync();
            return med;
        }

        public async Task<bool> DeleteMedicineAsync(int id)
        {
            var med = await _context.Medicines.FindAsync(id);
            if (med == null)
            {
                return false;
            }
            _context.Medicines.Remove(med);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Medicine> GetMedicineByIdAsync(int id)
        {
            return await _context.Medicines.Include(c => c.Category).Where(m=>m.Id==id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Medicine>> GetMedicinesAsync()
        {
            return await _context.Medicines.Include(c => c.Category).ToListAsync();
        }

        public async Task<Medicine> UpdateMedicineAsync(int id, Medicine medicine)
        {
            var existingMed = await _context.Medicines.FindAsync(id);
            if (existingMed == null)
            {
                return null;
            }
            _context.Entry(existingMed).CurrentValues.SetValues(medicine);
            await _context.SaveChangesAsync();
            var updatedMed = await _context.Medicines.Include(c => c.Category).Where(m => m.Id == id).FirstOrDefaultAsync();
            return updatedMed;
        }
    }
}
