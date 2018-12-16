using FITHAUI.ShoppingCart.UI.Models;
using FITHAUI.ShoppingCart.UI.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FITHAUI.ShoppingCart.UI.Controllers
{
    public class ProductController : BaseController
    {
        ProductRepository productRepository = new ProductRepository();
        CategoryRepository categoryRepository = new CategoryRepository();
        private readonly IHostingEnvironment _evn;
        public ProductController(IHostingEnvironment evn)
        {
            _evn = evn;
        }
        /// <summary>
        /// Lấy toàn bộ ds sản phẩm
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize]
        public IActionResult GetAllProductList(string startDate, string endDate, int? page)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ModelState.Clear();
            var model = productRepository.GetAllProducts(startDate, endDate);
            if (model == null)
            {
                return View("EmptyProduct");
            }
            else
            {
                return View("Index", model.ToPagedList(pageIndex, pageSize));
            }
        }
        [Route("danh-sach-san-pham")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("them-san-pham")]
        public IActionResult InsertProduct()
        {
            ViewBag.ListCategory = categoryRepository.GetCategories();
            return View("InsertProduct");
        }
        /// <summary>
        /// /Thêm sản phẩm
        /// </summary>
        /// <param name="product"></param>
        /// <param name="ProductImage"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreatedProduct(Product product, IFormFile ProductImage, string returnUrl = null)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (ModelState.IsValid)
                    {
                        //Danger(string.Format("<b>{0}</b> Thêm sản phẩm thất bại!", product.ProductCode), false);
                        return View("InsertProduct");
                    }
                    else
                    {
                        ProductRepository productRepository = new ProductRepository();
                        if (ProductImage != null && product != null)
                        {
                            var fileName = Path.Combine(_evn.WebRootPath
                                + Path.DirectorySeparatorChar.ToString()
                                + "images" + Path.DirectorySeparatorChar.ToString()
                                + "Product", Path.GetFileName(ProductImage.FileName));
                            using (var stream = new FileStream(fileName, FileMode.Create))
                            {
                                await ProductImage.CopyToAsync(stream);
                            }
                            if (productRepository.InsertProduct(product, ProductImage))
                            {
                                //Success(string.Format("<b>{0}</b> Thêm sản phẩm thành công!", product.ProductCode), true);
                                return Redirect("GetAllProductList");
                            }
                            else
                            {
                                //Danger(string.Format("<b>{0}</b> Thêm sản phẩm thất bại!", product.ProductCode), false);
                                return View("InsertProduct");
                            }
                        }
                        else
                        {
                            //Danger(string.Format("<b>{0}</b> Thêm sản phẩm thất bại!", product.ProductCode), false);
                            return View("InsertProduct");
                        }
                    }
                }
                else
                {
                    //Warning(string.Format("Bạn chưa đăng nhập!"), true);
                    return Redirect("/Identity/Account/Login");
                }
            }
            catch (Exception ex)
            {
                Warning(string.Format("Thêm thất bại!"), true);
                Console.WriteLine(ex.Message);
                return View("InsertProduct");
            }
        }
        /// <summary>
        /// Lấy tên sản phẩm theo ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IActionResult GetProductByProcductCode(string productId)
        {
            var model = productRepository.GetProductByProcductCode(productId);
            ViewBag.ListCategory = categoryRepository.GetCategories();
            return View("EditProduct", ViewBag.ListCategory);
        }
        /// <summary>
        /// Sửa sản phẩm
        /// </summary>
        /// <param name="product"></param>
        /// <param name="ProductImage"></param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateProduct(Product product, IFormFile ProductImage)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (ModelState.IsValid)
                    {
                        //Danger(string.Format("<b>{0}</b> Thêm sản phẩm thất bại!", product.ProductCode), false);
                        return View("InsertProduct");
                    }
                    else
                    {
                        ProductRepository productRepository = new ProductRepository();
                        if (ProductImage != null && product != null)
                        {
                            var fileName = Path.Combine(_evn.WebRootPath
                                + Path.DirectorySeparatorChar.ToString()
                                + "images" + Path.DirectorySeparatorChar.ToString()
                                + "Product", Path.GetFileName(ProductImage.FileName));
                            using (var stream = new FileStream(fileName, FileMode.Create))
                            {
                                await ProductImage.CopyToAsync(stream);
                            }
                            if (productRepository.UpdateProduct(product, ProductImage))
                            {
                                //Success(string.Format("<b>{0}</b> Thêm sản phẩm thành công!", product.ProductCode), true);
                                return Redirect("GetAllProductList");
                            }
                            else
                            {
                                //Danger(string.Format("<b>{0}</b> Thêm sản phẩm thất bại!", product.ProductCode), true);
                                return Redirect("InsertProduct");
                            }
                        }
                        else
                        {
                            //Danger(string.Format("<b>{0}</b> Thêm sản phẩm thất bại!", product.ProductCode), false);
                            return View("InsertProduct");
                        }
                    }
                }
                else
                {
                    //Warning(string.Format("Bạn chưa đăng nhập!"), false);
                    return Redirect("/Identity/Account/Login");
                }
            }
            catch (Exception ex)
            {
                Warning(string.Format("Thêm thất bại!"), false);
                Console.WriteLine(ex.Message);
                return View("InsertProduct");
            }
        }
        /// <summary>
        /// Xóa sản phẩm
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IActionResult DeleteProduct(int productId)
        {
            var check = productRepository.DeleteProduct(productId);
            if (check)
            {
                //Success(string.Format("Xóa sản phẩm {0} thất bại!", productId), true);
                return Redirect("GetAllProductList");
            }
            else
            {
                //Danger(string.Format("Xóa sản phẩm {0} thất bại!", productId), false);
                return Redirect("GetAllProductList");
            }
        }
        public IActionResult GetProductByCategoryId(string categoryId)
        {
            var model = productRepository.GetProductByCategoryId(categoryId);
            return PartialView("~/Views/Home/_ProductByCategory.cshtml", model);
        }
    }
}