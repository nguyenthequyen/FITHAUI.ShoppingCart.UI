using Microsoft.AspNetCore.Mvc;

namespace FITHAUI.ShoppingCart.UI.Controllers
{
    public class CartController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewCart()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }
    }
}