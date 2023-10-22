using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEcommerce_DB
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int MedId { get; set; }
        [ForeignKey("MedId")]
        public Medicine? Medicine { get; set; }
        public int Quantity { get; set; }
        
    }
}
