using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FITHAUI.ShoppingCart.UI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FITHAUI.ShoppingCart.UI.Controllers
{
    public class ManagerCartController : Controller
    {
        ManagerCartRepository managerCartRepository = new ManagerCartRepository();
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult GetCart(string startDate, string endDate, int? page)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ModelState.Clear();
            var model = managerCartRepository.GetCart(startDate, endDate);
            if (model == null)
            {
                return View("EmptyProduct");
            }
            else
            {
                return View("Index", model.ToPagedList(pageIndex, pageSize));
            }
        }
    }
}