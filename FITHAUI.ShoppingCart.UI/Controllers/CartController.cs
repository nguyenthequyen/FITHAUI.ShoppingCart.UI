using FITHAUI.ShoppingCart.UI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FITHAUI.ShoppingCart.UI.Controllers
{
    public class CartController : BaseController
    {
        ProductRepository productRepository = new ProductRepository();
        HomeRepository homeRepository = new HomeRepository();
        CategoryRepository categoryRepository = new CategoryRepository();
        public IActionResult Index()
        {
            return View();
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
    }
}