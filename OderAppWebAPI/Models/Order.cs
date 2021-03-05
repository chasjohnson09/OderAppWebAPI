using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OderAppWebAPI.Models
{
    public class Order
    {
        public Order() { }      // default contructor

        public int Id { get; set; }     
        [StringLength(200), Required]       // required is making the property NOT nullable
        public string Description { get; set; }
        [StringLength(10), Required]
        public string OrderStatus { get; set; } = "NEW";
        [Column(TypeName = "decimal (9,2)")]            // column is how to make an int into decimal
        public decimal OrderTotal { get; set; }
        public int CustomerId { get; set; }     // VS automatically recognizes this as a FOREIGN KEY
        public virtual Customer Customer { get; set; }
        public virtual IEnumerable<Orderline> Orderlines { get; set; }  // generic collection to be able to for each through all of them
        public int? SalesPersonId { get; set; } 
        public virtual SalesPerson SalesPerson { get; set; }
    }
}
