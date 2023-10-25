using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedEcommerce_DB;

namespace MedEcommerce_Core
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> UpdateCategoryAsync(int id, Category category);
        Task<Category> CreateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
