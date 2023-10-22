using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEcommerce_DB
{
    public class Cart
    {
        public int ID { get; set; }
        public int MedId { get; set; }
        [ForeignKey("MedId")]
        public Medicine? Medicine { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
