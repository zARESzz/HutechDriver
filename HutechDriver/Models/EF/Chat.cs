namespace HutechDriver.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Chat")]
    public partial class Chat
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string UserID { get; set; }

        [Required]
        [StringLength(255)]
        public string Gmail { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime? DateCreate { get; set; }

        [Required]
        public string Writing { get; set; }

        public bool IsRead { get; set; }
    }
}
