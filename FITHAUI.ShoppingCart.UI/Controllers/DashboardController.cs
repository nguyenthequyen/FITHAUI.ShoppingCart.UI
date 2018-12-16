using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FITHAUI.ShoppingCart.UI.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize]
        [Route("Admin")]
        public IActionResult Index()
        {
            return View("/Views/Dashboard/Dashboard.cshtml");
        }
    }
}