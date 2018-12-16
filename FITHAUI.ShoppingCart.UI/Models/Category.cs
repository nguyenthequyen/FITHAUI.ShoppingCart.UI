using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FITHAUI.ShoppingCart.UI.Models
{
    public class Category
    {
        public List<SelectListItem> Categories { get; set; }
        /// <summary>
        /// ID loại sản phẩm
        /// </summary>
        [DisplayName("Mã loại sản phẩm")]
        [Required(ErrorMessage = "Loại sản phẩm không được để trống")]
        public int CategoryId { get; set; }
        /// <summary>
        /// Tên loại sản phẩm
        /// </summary>
        [DisplayName("Loại sản phẩm")]
        [Required(ErrorMessage ="Loại sản phẩm không được để trống")]
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public Category(List<SelectListItem> categories, int categoryId, string categoryName, ICollection<Product> products)
        {
            Categories = categories;
            CategoryId = categoryId;
            CategoryName = categoryName;
            Products = products;
        }

        public Category()
        {
        }
    }
}
