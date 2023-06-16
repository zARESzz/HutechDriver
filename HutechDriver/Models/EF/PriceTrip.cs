using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HutechDriver.Models.EF
{
    [Table("PriceTrip")]
    public class PriceTrip
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int PriceLow { get; set; }
    }
}