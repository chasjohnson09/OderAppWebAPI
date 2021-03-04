using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OderAppWebAPI.Models
{
    public class Customer
    {
        public int ID { get; set; }
        [StringLength(10), Required]
        public string Code { get; set; }
        [StringLength(50), Required]
        public string Name { get; set; }
        public bool IsNational { get; set; }
        [Column(TypeName = "Decimal(9,2)")]
        public decimal Sales { get; set; }
        public DateTime? Created { get; set; }
    }
}
