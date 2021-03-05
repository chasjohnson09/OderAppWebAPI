using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OderAppWebAPI.Models
{
    public class SalesPerson
    {
        public SalesPerson() { }
        public int Id { get; set; }
        [StringLength(30), Required]
        public string Name { get; set; }
        [StringLength(2), Required]
        public string Statecode { get; set; }
        [Column(TypeName = "Decimal (9,2)")]
        public decimal Sales { get; set; }
        public int OrderId { get; set; }    // need to get rid of this !!!!!
        

    }
}
