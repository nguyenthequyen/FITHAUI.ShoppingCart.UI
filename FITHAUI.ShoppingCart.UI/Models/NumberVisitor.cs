using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FITHAUI.ShoppingCart.UI.Models
{
    public class NumberVisitor
    {
        /// <summary>
        /// ID số lượt truy cập
        /// </summary>
        public Guid NumberVisitsId { get; set; }
        /// <summary>
        /// Số lượt truy cập
        /// </summary>
        public int NumberVisitAmount { get; set; }
    }
}
