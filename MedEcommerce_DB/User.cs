using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEcommerce_DB
{
    public class User
    {
        public int Id { get; set; }

        
        public string? FirstName { get; set; }

       
        public string? LastName { get; set; }

        [Required]
        public string? UserEmail { get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set;}

        public int ? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public UserRole? Role { get; set; }

    }
}
