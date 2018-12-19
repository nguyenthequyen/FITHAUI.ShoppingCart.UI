using FITHAUI.ShoppingCart.UI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FITHAUI.ShoppingCart.UI.Repository
{
    public class CategoryRepository
    {
        public static string GetConnectionString()
        {
            return Startup.ConnectionString;
        }
        string connectionString = GetConnectionString();
        /// <summary>
        /// Danh mục sản phẩm
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCategories()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_GetCategory", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        categories.Add(new SelectListItem
                        {
                            Text = sqlDataReader["CategoryName"].ToString(),
                            Value = sqlDataReader["CategoryId"].ToString()
                        });
                    }
                    sqlConnection.Close();
                }
                return categories;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public IEnumerable<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_GetCategory", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryName = sqlDataReader["CategoryName"].ToString(),
                            CategoryCode = sqlDataReader["CategoryCode"].ToString(),
                            CategoryId = int.Parse(sqlDataReader["CategoryId"].ToString())
                        });
                    }
                    sqlConnection.Close();
                }
                return categories;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return categories;
        }
        public Category GetCategoryByCategoryId(int categoryId)
        {
            Category categorie = new Category();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_GetCategoryById", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CategoryId", categoryId);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        categorie.CategoryId = int.Parse(sqlDataReader["CategoryId"].ToString());
                        categorie.CategoryName = sqlDataReader["CategoryName"].ToString();
                        categorie.CategoryCode = sqlDataReader["CategoryCode"].ToString();
                    }
                    sqlConnection.Close();
                }
                return categorie;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return categorie;
        }
        public bool CreatedCategory(Category category)
        {
            bool check = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_InsertCategory", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CategoryCode", category.CategoryCode);
                    sqlCommand.Parameters.AddWithValue("@CategoryName", category.CategoryName);
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
        }public bool EditCategory(Category category)
        {
            bool check = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_UpdateCategory ", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CategoryCode", category.CategoryCode);
                    sqlCommand.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    sqlCommand.Parameters.AddWithValue("@CategoryId", category.CategoryId);
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
    }
}

