using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEcommerce_DB
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Medicine Type")]
        public string? MedicineType { get; set; }
        public bool? Deleted { get; set; }
    }
}
