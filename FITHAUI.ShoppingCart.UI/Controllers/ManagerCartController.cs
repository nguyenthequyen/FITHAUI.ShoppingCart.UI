using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FITHAUI.ShoppingCart.UI.Controllers
{
    public class ManagerCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}