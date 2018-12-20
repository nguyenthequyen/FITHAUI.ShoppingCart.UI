using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FITHAUI.ShoppingCart.UI.Models
{
    public class CartDetail
    {
        /// <summary>
        /// Id chi tiết giỏ hàng
        /// </summary>
        public int CartDetailId { get; set; }
        /// <summary>
        /// Id sản phẩm
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Id giỏ hàng
        /// </summary>
        public int CartId { get; set; }
        /// <summary>
        /// Số lượng sản phẩm
        /// </summary>
        public int Amount { get; set; }
    }
}
