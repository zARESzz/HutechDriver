using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HutechDriver.Models.EF
{
    [Table("tb_Order")]
    public class Order:CommonAbstract
    {
        public Order()
        {
            this.OrderDetails=new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        public string CustomerId { get; set; }
        
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public int TypePayment { get; set; }
    }
}