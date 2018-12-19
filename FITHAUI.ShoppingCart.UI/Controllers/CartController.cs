using Microsoft.AspNetCore.Mvc;
using FITHAUI.ShoppingCart.UI.Repository;
using FITHAUI.ShoppingCart.UI.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using FITHAUI.ShoppingCart.UI.Infrastructure;

namespace FITHAUI.ShoppingCart.UI.Controllers
{
    public class CartController : BaseController
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult ViewCart()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }

        public ProductRepository repo;

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        public void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repo.GetProductByProcductCode(productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repo.GetProductByProcductCode(productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

    }
}