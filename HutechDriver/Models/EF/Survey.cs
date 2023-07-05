using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HutechDriver.Models.EF
{
    [Table("Survey")]
    public class Survey
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }
        public string FullName { get; set; }
        [Required(ErrorMessage = "Không được để trống tiêu đề ")]
        [StringLength(150)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Không được để trống mô tả ngắn ")]

        public string Description { get; set; }
        public string DescriptionFull { get; set; }

        [Required(ErrorMessage = "Không được để trống link Form")]
        [StringLength(1000)]
        public string Link { get; set; }
        public int PointSurvey { get; set; }
        public DateTime? CreateDate { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}