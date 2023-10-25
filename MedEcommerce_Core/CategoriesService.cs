using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedEcommerce_DB;
using Microsoft.EntityFrameworkCore;

namespace MedEcommerce_Core
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext _context;
        public CategoriesService(ApplicationDbContext context) {  _context = context; }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
                return null;

            _context.Entry(existingCategory).CurrentValues.SetValues(category);
            await _context.SaveChangesAsync();
            return existingCategory;
        }
    }
}
