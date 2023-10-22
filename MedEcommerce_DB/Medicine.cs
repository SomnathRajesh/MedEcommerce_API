using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedEcommerce_DB
{
    public class Medicine
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public decimal? Price { get; set; }

        public string? Image { get; set; }
        public string? Seller { get; set; }

        public bool? Deleted { get; set; }

        [Required]
        [Display(Name = "Available")]
        public bool? IsAvailable { get; set; }

        [Required]
        [Display(Name = "Medicine Type")]
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]

        public virtual Category? Category { get; set; }
    }
}