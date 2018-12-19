using FITHAUI.ShoppingCart.UI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FITHAUI.ShoppingCart.UI.Models
{
    public class Cart
    {
        public List<CartLine> listLine = new List<CartLine>();

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
            return listLine.Sum(e => e.Product.ProductPrice * e.Quantity);
        }

        public void Clear()
        {
            listLine.Clear();
        }

        public IEnumerable<CartLine> Lines => listLine;
    }
}
