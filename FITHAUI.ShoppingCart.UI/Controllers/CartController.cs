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

    public class CartController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        HomeRepository homeRepository = new HomeRepository();
        CategoryRepository categoryRepository = new CategoryRepository();
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartLine>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                ViewBag.cart = cart;
                ViewBag.quanty = cart.Sum(x => x.Quantity);
                ViewBag.total = cart.Sum(item => item.Product.ProductPrice * item.Quantity * (100 - item.Product.ProductSale) / 100);
                ViewBag.Category = categoryRepository.GetAllCategories();
            }
            else
            {
                ViewBag.Category = categoryRepository.GetAllCategories();
            }
            return View("ViewCart");
        }

        public RedirectToActionResult AddToCart(int productId)
        {
            if (SessionHelper.GetObjectFromJson<List<CartLine>>(HttpContext.Session, "cart") == null)
            {
                List<CartLine> cart = new List<CartLine>();
                cart.Add(new CartLine { Product = productRepository.GetProductByProcductCode(productId), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartLine> cart = SessionHelper.GetObjectFromJson<List<CartLine>>(HttpContext.Session, "cart");
                int index = isExist(productId);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartLine { Product = productRepository.GetProductByProcductCode(productId), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId)
        {
            List<CartLine> cart = SessionHelper.GetObjectFromJson<List<CartLine>>(HttpContext.Session, "cart");
            int index = isExist(productId);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(int productId)
        {
            List<CartLine> cart = SessionHelper.GetObjectFromJson<List<CartLine>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId.Equals(productId))
                {
                    return i;
                }
            }
            return -1;
        }
        public IActionResult ViewCart()
        {
            ViewBag.ProductNew = productRepository.GetProductsNew();
            ViewBag.ProductHost = productRepository.GetProductsHot();
            ViewBag.Category = categoryRepository.GetAllCategories();
            return RedirectToAction("Index");
        }
        public IActionResult CheckOut()
        {
            ViewBag.ProductNew = productRepository.GetProductsNew();
            ViewBag.ProductHost = productRepository.GetProductsHot();
            ViewBag.Category = categoryRepository.GetAllCategories();
            return View("CheckOut");
        }

    }
}
