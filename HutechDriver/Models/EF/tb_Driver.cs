using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace HutechDriver.Models.EF
{
    public class tb_Driver
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string DateofBirth { get; set; }

        public string Phone { get; set; }

        public string Image { get; set; }

        public string ImageCccd { get; set; }

        public string ImageBike { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifierBy { get; set; }
    }
}
