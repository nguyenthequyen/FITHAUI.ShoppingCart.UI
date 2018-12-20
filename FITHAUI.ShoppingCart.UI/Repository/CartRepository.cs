using FITHAUI.ShoppingCart.UI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FITHAUI.ShoppingCart.UI.Repository
{
    public class CartRepository
    {
        public static string GetConnectionString()
        {
            return Startup.ConnectionString;
        }
        string connectionString = GetConnectionString();

        public void InsertCart(Cart cart)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    var sql = "Insert into Cart(Name, Phone, Address, Email, AllProduct, AllPrice) values(@Name, @Phone, @Address, @Email, @AllProduct, @AllPrice)";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@Name", cart.Name);
                    sqlCommand.Parameters.AddWithValue("@Phone", cart.Phone);
                    sqlCommand.Parameters.AddWithValue("@Address", cart.Address);
                    sqlCommand.Parameters.AddWithValue("@Email", cart.Email);
                    sqlCommand.Parameters.AddWithValue("@AllProduct", cart.AllProduct);
                    sqlCommand.Parameters.AddWithValue("@AllPrice", cart.AllPrice);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void InsertCartDetails(CartDetail cartDetail)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    var sql = "Insert into CartDetail(ProductId, CartId, Amount) values(@ProductId, @CartId, @Amount)";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ProductId", cartDetail.ProductId);
                    sqlCommand.Parameters.AddWithValue("@CartId", cartDetail.CartId);
                    sqlCommand.Parameters.AddWithValue("@Amount", cartDetail.Amount);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public int GetCartID()
        {
            CartDetail cart = new CartDetail();
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    var sql = "select top 1 CartId from Cart order by CartId desc";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        cart.CartId = int.Parse(sqlDataReader["CartId"].ToString());
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cart.CartId;
        }
    }
}
