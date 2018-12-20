using System;
﻿using FITHAUI.ShoppingCart.UI.Repository;
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
        public decimal? AllPrice { get; set; }
        public List<CartLine> listLine = new List<CartLine>();

        public Cart(string name, string phone, string address, string email, int allProduct, decimal? allPrice)
        {
            Name = name;
            Phone = phone;
            Address = address;
            Email = email;
            AllProduct = allProduct;
            AllPrice = allPrice;
        }
        public Cart()
        {

        }
        public void AddItem(Product product, int quantity)
        {
            CartLine line = listLine.Where(p => p.Product.ProductId == product.ProductId).FirstOrDefault();
            if (line == null)
            {
                listLine.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            listLine.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        public Decimal? ComputeTotalValue()
        {
            return listLine.Sum(e => e.Product.ProductPrice * e.Quantity * (100 - e.Product.ProductSale) /100 );
        }

        public int TotalProduct()
        {
            return listLine.Sum(e => e.Quantity);
        }

        public void Clear()
        {
            listLine.Clear();
        }

        public IEnumerable<CartLine> Lines => listLine;
    }
}
