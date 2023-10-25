using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedEcommerce_DB;

namespace MedEcommerce_Core
{
    public interface IMedicineService
    {
        Task<IEnumerable<Medicine>> GetMedicinesAsync();
        Task<Medicine> GetMedicineByIdAsync(int id);
        Task<Medicine> UpdateMedicineAsync(int id, Medicine medicine);
        Task<Medicine> CreateMedicineAsync(Medicine medicine);
        Task<bool> DeleteMedicineAsync(int id);

    }
}
