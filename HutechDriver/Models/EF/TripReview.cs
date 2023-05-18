using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HutechDriver.Models.EF
{
    public class TripReview
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Rating { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Comment { get; set; }

        public virtual Trip Trip { get; set; }
    }
}