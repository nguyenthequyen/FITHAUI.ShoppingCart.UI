using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FITHAUI.ShoppingCart.UI.Models;
using FITHAUI.ShoppingCart.UI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FITHAUI.ShoppingCart.UI.Controllers
{
    public class CategoryController : BaseController
    {
        CategoryRepository categoryRepository = new CategoryRepository();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetCategories()
        {
            var category = categoryRepository.GetCategories();
            return PartialView("~Views/Category/_CategoryPartial.cshtml", category);
        }
    }
}