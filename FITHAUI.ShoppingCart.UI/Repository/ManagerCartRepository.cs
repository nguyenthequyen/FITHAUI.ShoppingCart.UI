using FITHAUI.ShoppingCart.UI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FITHAUI.ShoppingCart.UI.Repository
{
    public class ManagerCartRepository
    {
        public static string GetConnectionString()
        {
            return Startup.ConnectionString;
        }
        string connectionString = GetConnectionString();

        public IEnumerable<Cart> GetCart(string startDate, string endDate)
        {
            try
            {
                List<Cart> carts = new List<Cart>();
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("Proc_GetCart", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Cart cart = new Cart();
                        cart.Name= sqlDataReader["Name"].ToString();
                        cart.Phone= sqlDataReader["Phone"].ToString();
                        cart.Address = sqlDataReader["Address"].ToString();
                        cart.Email= sqlDataReader["Email"].ToString();
                        cart.AllPrice = Decimal.Parse(sqlDataReader["AllPrice"].ToString());
                        cart.AllProduct = int.Parse(sqlDataReader["AllProduct"].ToString());
                        carts.Add(cart);
                    }
                    sqlConnection.Close();
                }
                return carts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
