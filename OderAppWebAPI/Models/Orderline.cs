using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OderAppWebAPI.Models
{
    public class Orderline
    {
        public Orderline() { }


        public int Id { get; set; }
        

        public int OrderId { get; set; }
        [JsonIgnore]
        public  virtual Order Order { get; set;}
        

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        
        
        public int Qunatity { get; set; }
    }
}
