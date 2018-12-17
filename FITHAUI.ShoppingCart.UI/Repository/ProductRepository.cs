using FITHAUI.ShoppingCart.UI.Data;
using FITHAUI.ShoppingCart.UI.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FITHAUI.ShoppingCart.UI.Repository
{
    public class ProductRepository
    {
        public static string GetConnectionString()
        {
            return Startup.ConnectionString;
        }
        string connectionString = GetConnectionString();
        /// <summary>
        /// Lấy danh sách sản phẩm mới nhất
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProductsNew()
        {
            try
            {
                List<Product> products = new List<Product>();
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_GetProductNew", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Product product = new Product();
                        product.ProductName = sqlDataReader["ProductName"].ToString();
                        product.ProductImage = sqlDataReader["ProductImage"].ToString();
                        product.ProductPrice = Decimal.Parse(sqlDataReader["ProductPrice"].ToString());
                        product.ProductRatting = int.Parse(sqlDataReader["ProductRatting"].ToString());
                        product.ProductColor = sqlDataReader["ProductColor"].ToString();
                        product.ProductId = int.Parse(sqlDataReader["ProductId"].ToString());
                        product.ProductSale = int.Parse(sqlDataReader["ProductSale"].ToString());
                        products.Add(product);
                    }
                    sqlConnection.Close();
                }
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public IEnumerable<Product> GetProductsHot()
        {
            try
            {
                List<Product> products = new List<Product>();
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_GetProductHot", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Product product = new Product();
                        product.ProductName = sqlDataReader["ProductName"].ToString();
                        product.ProductId = int.Parse(sqlDataReader["ProductId"].ToString());
                        product.ProductImage = sqlDataReader["ProductImage"].ToString();
                        product.ProductPrice = Decimal.Parse(sqlDataReader["ProductPrice"].ToString());
                        product.ProductRatting = int.Parse(sqlDataReader["ProductRatting"].ToString());
                        product.ProductColor = sqlDataReader["ProductColor"].ToString();
                        product.CategoryId = int.Parse(sqlDataReader["CategoryId"].ToString());
                        product.ProductSale = int.Parse(sqlDataReader["ProductSale"].ToString());
                        products.Add(product);
                    }
                    sqlConnection.Close();
                }
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public IEnumerable<Product> ProductDetails(int productId)
        {
            try
            {
                List<Product> products = new List<Product>();
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_GetProductDetailByProductId", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ProductId", productId);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Product product = new Product();
                        product.ProductName = sqlDataReader["ProductName"].ToString();
                        product.ProductImage = sqlDataReader["ProductImage"].ToString();
                        product.ProductPrice = Decimal.Parse(sqlDataReader["ProductPrice"].ToString());
                        product.ProductRatting = int.Parse(sqlDataReader["ProductRatting"].ToString());
                        product.ProductColor = sqlDataReader["ProductColor"].ToString();
                        product.ProductCode = sqlDataReader["ProductCode"].ToString();
                        product.CategoryId = int.Parse(sqlDataReader["CategoryId"].ToString());
                        product.ProductDescriptionLong = sqlDataReader["ProductDescriptionLong"].ToString();
                        product.ProductBrand = sqlDataReader["ProductBrand"].ToString();
                        product.ProductStatus = int.Parse(sqlDataReader["ProductStatus"].ToString());
                        product.ProductSale = int.Parse(sqlDataReader["ProductSale"].ToString());
                        products.Add(product);
                    }
                    sqlConnection.Close();
                }
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Thêm sản phẩm
        /// </summary>
        /// <param name="product"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool InsertProduct(Product product, IFormFile file)
        {
            bool check = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_InsetProduct", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ProductName", product.ProductName);
                    sqlCommand.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
                    sqlCommand.Parameters.AddWithValue("@ProductColor", product.ProductColor);
                    sqlCommand.Parameters.AddWithValue("@ProductImage", file.FileName);
                    sqlCommand.Parameters.AddWithValue("@ProductDescriptionLong", product.ProductDescriptionLong);
                    sqlCommand.Parameters.AddWithValue("@ProductDescriptionShort", product.ProductDescriptionShort);
                    sqlCommand.Parameters.AddWithValue("@ProductAmount", product.ProductAmount);
                    sqlCommand.Parameters.AddWithValue("@ProductStatus", product.ProductStatus);
                    sqlCommand.Parameters.AddWithValue("@ProductCode", product.ProductCode);
                    sqlCommand.Parameters.AddWithValue("@ProductNew", product.ProductNew);
                    sqlCommand.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    check = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                check = false;
            }
            return check;
        }
        public IEnumerable<Product> GetAllProducts(string startDate, string endDate)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_GetAllProductList", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    //sqlCommand.Parameters.AddWithValue("@StartDate", "01/01/2018");
                    //sqlCommand.Parameters.AddWithValue("@EndDate", "12/12/2018");
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Product product = new Product();
                        product.ProductName = sqlDataReader["ProductName"].ToString();
                        product.ProductId = int.Parse(sqlDataReader["ProductId"].ToString());
                        product.ProductPrice = Decimal.Parse(sqlDataReader["ProductPrice"].ToString());
                        product.ProductColor = sqlDataReader["ProductColor"].ToString();
                        product.ProductImage = sqlDataReader["ProductImage"].ToString();
                        product.ProductDescriptionLong = sqlDataReader["ProductDescriptionLong"].ToString();
                        product.ProductDescriptionShort = sqlDataReader["ProductDescriptionShort"].ToString();
                        product.ProductAmount = int.Parse(sqlDataReader["ProductAmount"].ToString());
                        product.ProductStatus = int.Parse(sqlDataReader["ProductStatus"].ToString());
                        product.ProductRatting = int.Parse(sqlDataReader["ProductRatting"].ToString());
                        product.ProductNew = int.Parse(sqlDataReader["ProductNew"].ToString());
                        product.ProductCode = sqlDataReader["ProductCode"].ToString();
                        product.ProductBrand = sqlDataReader["ProductBrand"].ToString();
                        products.Add(product);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return products;
        }

        public Product GetProductByProcductCode(int productId)
        {
            Product product = new Product();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_GetProductByProductCode", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ProductId", productId);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        product.ProductName = sqlDataReader["ProductName"].ToString();
                        product.ProductPrice = Decimal.Parse(sqlDataReader["ProductPrice"].ToString());
                        product.ProductColor = sqlDataReader["ProductColor"].ToString();
                        product.ProductImage = sqlDataReader["ProductImage"].ToString();
                        product.ProductDescriptionLong = sqlDataReader["ProductDescriptionLong"].ToString();
                        product.ProductDescriptionShort = sqlDataReader["ProductDescriptionShort"].ToString();
                        product.ProductAmount = int.Parse(sqlDataReader["ProductAmount"].ToString());
                        product.ProductStatus = int.Parse(sqlDataReader["ProductStatus"].ToString());
                        product.ProductNew = int.Parse(sqlDataReader["ProductNew"].ToString());
                        product.ProductCode = sqlDataReader["ProductCode"].ToString();
                        product.CategoryId = int.Parse(sqlDataReader["CategoryId"].ToString());
                        product.ProductId = int.Parse(sqlDataReader["ProductId"].ToString());
                        product.ProductBrand = sqlDataReader["ProductBrand"].ToString();
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi xảy ra: " + ex.Message);
                throw;
            }
            return product;
        }
        public bool UpdateProduct(Product product, IFormFile ProductImage)
        {
            var check = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_UpdateProduct", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ProductCode", product.ProductCode);
                    sqlCommand.Parameters.AddWithValue("@ProductName", product.ProductName);
                    sqlCommand.Parameters.AddWithValue("@ProductNew", product.ProductNew);
                    sqlCommand.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
                    sqlCommand.Parameters.AddWithValue("@ProductStatus", product.ProductStatus);
                    sqlCommand.Parameters.AddWithValue("@ProductDescriptionShort", product.ProductDescriptionShort);
                    sqlCommand.Parameters.AddWithValue("@ProductDescriptionLong", product.ProductDescriptionLong);
                    sqlCommand.Parameters.AddWithValue("@ProductColor", product.ProductColor);
                    sqlCommand.Parameters.AddWithValue("@ProductAmount", product.ProductAmount);
                    sqlCommand.Parameters.AddWithValue("@ProductImage", ProductImage.FileName);
                    sqlCommand.Parameters.AddWithValue("@ProductId", product.ProductId);
                    sqlCommand.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    check = true;
                }
            }
            catch (Exception ex)
            {
                check = false;
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return check;
        }
        public bool DeleteProduct(int productId)
        {
            var check = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_DeleteProduct", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ProductId", productId);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    check = true;
                }
            }
            catch (Exception ex)
            {
                check = false;
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return check;
        }
        /// <summary>
        /// Lấy sản phẩm theo danh mục sản phẩm
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProductByCategoryId(string categoryName)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_GetProductByCategoryId", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CategoryName", categoryName);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Product product = new Product();
                        product.ProductName = sqlDataReader["ProductName"].ToString();
                        product.ProductPrice = Decimal.Parse(sqlDataReader["ProductPrice"].ToString());
                        product.ProductColor = sqlDataReader["ProductColor"].ToString();
                        product.ProductImage = sqlDataReader["ProductImage"].ToString();
                        product.ProductDescriptionLong = sqlDataReader["ProductDescriptionLong"].ToString();
                        product.ProductDescriptionShort = sqlDataReader["ProductDescriptionShort"].ToString();
                        product.ProductAmount = int.Parse(sqlDataReader["ProductAmount"].ToString());
                        product.ProductStatus = int.Parse(sqlDataReader["ProductStatus"].ToString());
                        product.ProductNew = int.Parse(sqlDataReader["ProductNew"].ToString());
                        product.ProductCode = sqlDataReader["ProductCode"].ToString();
                        product.CategoryId = int.Parse(sqlDataReader["CategoryId"].ToString());
                        product.ProductId = int.Parse(sqlDataReader["ProductId"].ToString());
                        product.ProductSale = int.Parse(sqlDataReader["ProductSale"].ToString());
                        products.Add(product);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return products;
        }
        public IEnumerable<Product> SearchProductByProductName(string productName)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_GetProductByProductName", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ProductName", productName);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Product product = new Product();
                        product.ProductName = sqlDataReader["ProductName"].ToString();
                        product.ProductPrice = Decimal.Parse(sqlDataReader["ProductPrice"].ToString());
                        product.ProductColor = sqlDataReader["ProductColor"].ToString();
                        product.ProductImage = sqlDataReader["ProductImage"].ToString();
                        product.ProductDescriptionLong = sqlDataReader["ProductDescriptionLong"].ToString();
                        product.ProductDescriptionShort = sqlDataReader["ProductDescriptionShort"].ToString();
                        product.ProductAmount = int.Parse(sqlDataReader["ProductAmount"].ToString());
                        product.ProductStatus = int.Parse(sqlDataReader["ProductStatus"].ToString());
                        product.ProductNew = int.Parse(sqlDataReader["ProductNew"].ToString());
                        product.ProductCode = sqlDataReader["ProductCode"].ToString();
                        product.CategoryId = int.Parse(sqlDataReader["CategoryId"].ToString());
                        product.ProductId = int.Parse(sqlDataReader["ProductId"].ToString());
                        products.Add(product);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return products;
        }
    }
}
