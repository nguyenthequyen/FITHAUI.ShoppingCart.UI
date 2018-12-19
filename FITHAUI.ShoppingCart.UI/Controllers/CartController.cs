using Microsoft.AspNetCore.Mvc;
using FITHAUI.ShoppingCart.UI.Repository;
using FITHAUI.ShoppingCart.UI.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using FITHAUI.ShoppingCart.UI.Infrastructure;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FITHAUI.ShoppingCart.UI.Controllers
{
    public class CartController : BaseController
    {
        ProductRepository productRepository = new ProductRepository();
        HomeRepository homeRepository = new HomeRepository();
        CategoryRepository categoryRepository = new CategoryRepository();
        public IActionResult Index(int productId)
        {
            ViewBag.Category = categoryRepository.GetMenuCategories();
            return View("ViewCart", new CartIndexViewModel
            {
                Cart = GetCart()
            });
        }
        public IActionResult ViewCart()
        {
            ViewBag.ProductNew = productRepository.GetProductsNew();
            ViewBag.ProductHost = productRepository.GetProductsHot();
            ViewBag.Category = categoryRepository.GetMenuCategories();
            return View();
        }
        public IActionResult CheckOut()
        {
            ViewBag.ProductNew = productRepository.GetProductsNew();
            ViewBag.ProductHost = productRepository.GetProductsHot();
            ViewBag.Category = categoryRepository.GetMenuCategories();
            return View();
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

        public RedirectToActionResult AddToCart(int productId)
        {
            Product product = productRepository.GetProductByProcductCode(productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromCart(int productId)
        {
            Product product = productRepository.GetProductByProcductCode(productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

    }
}