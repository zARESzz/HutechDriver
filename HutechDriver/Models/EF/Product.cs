using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HutechDriver.Models.EF
{
    [Table("tb_Product")]
    public class Product : CommonAbstract
    {
        public Product()
        {
            this.ProductImage= new HashSet<ProductImage>();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        [Required(ErrorMessage = "Ảnh không được để trống")]
        public string Image { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceSale { get; set; }
        public int Quantity { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsFeature { get; set; }
        public bool IsHot { get; set; }
        public int ProductCategoryID { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public bool IsActive { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection< ProductImage> ProductImage { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}