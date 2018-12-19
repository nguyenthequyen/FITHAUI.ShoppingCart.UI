using FITHAUI.ShoppingCart.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FITHAUI.ShoppingCart.UI.Repository
{
    public class ManagerCartRepository
    {
        public IEnumerable<Cart> GetCart(string startDate, string endDate)
        {
            List<Cart> carts = new List<Cart>();
            return carts;
        }
    }
}
