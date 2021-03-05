using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OderAppWebAPI.Models
{
    public class Item
    {
        public Item() { }

        public int Id { get; set; }
        [StringLength(30), Required]
        public string Name { get; set; }
        [Column(TypeName = "Decimal(9,2)")]
        public decimal Price { get; set; }
    }
}
