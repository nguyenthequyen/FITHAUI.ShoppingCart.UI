using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FITHAUI.ShoppingCart.UI.Models;
using FITHAUI.ShoppingCart.UI.Repository;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;

namespace FITHAUI.ShoppingCart.UI.Controllers
{
    public class HomeController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        HomeRepository homeRepository = new HomeRepository();
        CategoryRepository categoryRepository = new CategoryRepository();
        //Action Controller
        public IActionResult Index()
        {
            var count = homeRepository.GetNumberVisitor();
            ViewBag.NumberVisitAmount = 0;
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("countVisitor")))
            {
                count++;
                HttpContext.Session.SetInt32("countVisitor", count);
                homeRepository.UpdateNumberVisitor(count);
                ViewBag.NumberVisitAmount = homeRepository.GetNumberVisitor();
            }
            else
            {
                ViewBag.NumberVisitAmount = homeRepository.GetNumberVisitor();
            }
            ModelState.Clear();
            return View(productRepository.GetProducts());
        }

        //public IActionResult MenuPartial()
        //{
        //    var category = categoryRepository.GetCategories();
        //    return PartialView("~/Views/Home/MenuPartial.cshtml", category);
        //}

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
