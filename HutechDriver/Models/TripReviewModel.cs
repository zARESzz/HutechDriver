using HutechDriver.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HutechDriver.Models
{
    public class TripReviewModel
    {
        public int Id { get; set; }
        public string startLocation { get; set; }
        public string endLocation { get; set; }

        public string driverName { get; set; }

        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Comment { get; set; }

        public virtual Trip Trip { get; set; }
    }
}