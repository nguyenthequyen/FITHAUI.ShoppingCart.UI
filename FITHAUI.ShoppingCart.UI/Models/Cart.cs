using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FITHAUI.ShoppingCart.UI.Models
{
    public class Cart
    {
        /// <summary>
        /// Mã giỏ hàng
        /// </summary>
        public int CartId { get; set; }
        /// <summary>
        /// Id người dùng
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Tên người dùng
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Số lượng sản phẩm đã mua
        /// </summary>
        public int AllProduct { get; set; }
        /// <summary>
        /// Tổng giá
        /// </summary>
        public decimal AllPrice { get; set; }
    }
}
