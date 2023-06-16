using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HutechDriver.Models.EF
{
    [Table("tb_Contact")]
    public class Contact:CommonAbstract
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Tên không được để trống")]
        [StringLength(150,ErrorMessage ="Không được vượt quá 150 kí tự")]
        public string Name { get; set; }
        [StringLength(150, ErrorMessage = "Không được vượt quá 150 kí tự")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập số điện thoại của bạn")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(0|84)([0-9]{9})$", ErrorMessage = "Xin vui lòng nhập đúng định dạng số điện thoại")]
        public string PhoneNumber { get; set; }
        [StringLength(25, ErrorMessage = "Không được vượt quá  kí tự")]
        public string CCCDorCMND { get; set; }
        [Required(ErrorMessage = "Vui lòng để lại tin nhắn bạn muốn gửi")]
        [StringLength(4000, ErrorMessage = "Tin nhắn quá dài")]
        public string DiaChi { get; set; }
       
        public string ImagePeople { get; set; }
        
        public string ImagePapers { get; set; }
        [Required(ErrorMessage = "Vui lòng để lại tin nhắn bạn muốn gửi")]
        [StringLength(4000, ErrorMessage = "Tin nhắn quá dài")]
        public string Message { get; set; }
        public int IsRead { get; set; }
        public int IsStatus { get; set; }

    }
}