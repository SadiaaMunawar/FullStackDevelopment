using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using ProductManagementApp.Models;


namespace DataAccessLayer
{
    public class CRUD
    {
        public static List<ProductModel> GetAllData()
        {
            string Query = " select * from \"Products\"";
            List<ProductModel> products = new List<ProductModel>();

            NpgsqlConnection connection = DBHelper.GetConnection();
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(Query,connection);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()){
                ProductModel model = new ProductModel();
                    model.PId = Convert.ToInt32(reader["PId"]);
                    model.ProductName = reader["ProductName"].ToString();
                    model.Price = Convert.ToInt32(reader["Price"]);
                products.Add(model);
;            }

            connection.Close();
            return products;    
        }
    }
}
