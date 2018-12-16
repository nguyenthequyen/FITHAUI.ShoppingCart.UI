using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FITHAUI.ShoppingCart.UI.Models
{
    public class Product
    {
        /// <summary>
        /// ID sản phẩm
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        [DisplayName("Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm được để trống")]
        public string ProductName { get; set; }
        /// <summary>
        /// Giá tiền
        /// </summary>
        [DisplayName("Giá bán")]
        [Range(1, int.MaxValue, ErrorMessage = "Giá bán phải nằm trong khoảng {0} - {1}")]
        [Required(ErrorMessage = "Giá bán không được để trống")]
        public decimal? ProductPrice { get; set; }
        /// <summary>
        /// Số lượng sản phẩm
        /// </summary>
        [DisplayName("Số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải nằm trong khoảng {0} - {1}")]
        [Required(ErrorMessage = "Số lượng không được để trống")]
        public int? ProductAmount { get; set; }
        /// <summary>
        /// Màu sắc
        /// </summary>
        [DisplayName("Màu sắc")]
        [Required(ErrorMessage = "Màu sắc không được để trống")]
        public string ProductColor { get; set; }
        /// <summary>
        /// Hình ảnh sản phẩm
        /// </summary>
        [DisplayName("Hình ảnh sản phẩm")]
        [Required(ErrorMessage = "Hình ảnh không được để trống")]
        public string ProductImage { get; set; }
        /// <summary>
        /// Mô tả sản phẩm
        /// </summary>
        [DisplayName("Mô tả sản phẩm")]
        [Required(ErrorMessage = "Mô tả sản phẩm không được để trống")]
        public string ProductDescription { get; set; }
        /// <summary>
        /// Tình trạng sản phẩm
        /// </summary>
        [DisplayName("Tình trạng")]
        [Required(ErrorMessage = "Tình trạng không được để trống")]
        public int? ProductStatus { get; set; }
        /// <summary>
        /// Mã sản phẩm
        /// </summary>
        [DisplayName("Mã sản phẩm")]
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        public string ProductCode { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public int ModifyDate { get; set; }
        /// <summary>
        /// Đánh giá sản phẩm
        /// </summary>
        [DisplayName("Đánh giá")]
        public int ProductRatting { get; set; }
        [DisplayName("Sản phẩm mới")]
        public int ProductNew { get; set; }
        [DisplayName("Loại sản phẩm")]
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public Product()
        {
        }

        public Product(int productId, string productName, decimal? productPrice, int? productAmount, string productColor, string productImage, string productDescription, int? productStatus, string productCode, DateTime createdDate, int modifyDate, int productRatting, int productNew, int categoryId, Category category)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductAmount = productAmount;
            ProductColor = productColor;
            ProductImage = productImage;
            ProductDescription = productDescription;
            ProductStatus = productStatus;
            ProductCode = productCode;
            CreatedDate = createdDate;
            ModifyDate = modifyDate;
            ProductRatting = productRatting;
            ProductNew = productNew;
            CategoryId = categoryId;
            Category = category;
        }
    }
}
