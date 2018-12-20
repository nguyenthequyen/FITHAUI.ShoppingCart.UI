using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FITHAUI.ShoppingCart.UI.Models;
using FITHAUI.ShoppingCart.UI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

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
        public IActionResult GetAllCategories(int? page)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ModelState.Clear();
            var model = categoryRepository.GetAllCategories();
            if (model == null)
            {
                return View("EmptyProduct");
            }
            else
            {
                return View("~/Views/Category/GetAllCategories.cshtml", model.ToPagedList(pageIndex, pageSize));
            }
        }
        public IActionResult GetCategoryByCategoryId(int categoryId)
        {
            var model = categoryRepository.GetCategoryByCategoryId(categoryId);
            return View("~/Views/Category/EditCategory.cshtml", model);
        }
        public IActionResult EditCategory(Category category)
        {
            var model = categoryRepository.EditCategory(category);
            return Redirect("GetAllCategories");
        }
        public IActionResult CategoryDetails(string categoryId)
        {
            return View("~/Views/Category/CategoryDetails.cshtml");
        }
        public IActionResult InsertCategory()
        {
            return View("InsertCategory");
        }
        public IActionResult CreatedCategory(Category category)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (ModelState.IsValid)
                    {
                        TempData["success"] = "Thêm thành công!";
                        return View("GetAllCategories");
                    }
                    else
                    {
                        if (categoryRepository.CreatedCategory(category))
                        {
                            TempData["success"] = "Thêm thành công!";
                            return Redirect("GetAllCategories");
                        }
                        else
                        {
                            TempData["error"] = "Thêm thất bại!";
                            return Redirect("GetAllCategories");
                        }
                    }
                }
                else
                {
                    TempData["error"] = "Thêm thất bại!";
                    return Redirect("/Identity/Account/Login");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Thêm thất bại!";
                Console.WriteLine(ex.Message);
                return Redirect("GetAllCategories");
            }
        }
        /// <summary>
        /// Xóa sản phẩm
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Authorize]
        public IActionResult DeleteCategory(int categoryId)
        {
            var check = categoryRepository.DeleteCategory(categoryId);
            if (check)
            {
                TempData["success"] = "Xóa danh mục sản phẩm {0} thành công" + categoryId;
                return Redirect("GetAllCategories");
            }
            else
            {
                TempData["error"] = "Xóa danh mục sản phẩm {0} thất bại" + categoryId;
                return Redirect("GetAllCategories");
            }
        }
    }
}