using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SimpleWCFApplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class SimpleService : ISimpleService
    {
        //our basic get data function
        public IEnumerable<Product> GetProductData()
        {
            //Use ORM here in future?
            string connetionString = null; 
            string sql = null;

            List<Product> products = new List<Product>();

            SqlDataReader dataReader; 
            connetionString = "Data Source=AMOL-PC\\SQLEXPRESS;Initial Catalog=NORTHWND;User ID=sa;Password=sa123";
            sql = "Select ProductID,ProductName,UnitsInStock,Discontinued from products";
            using (var connection = new SqlConnection(connetionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var product = new Product();
                        product.ProductID = dataReader.GetInt32(0);
                        product.ProductName = dataReader.GetValue(1).ToString();
                        product.UnitInStock = dataReader.GetInt16(2);
                        product.Discontinued = dataReader.GetBoolean(3);
                        products.Add(product);
                    } 
                }    
            }
            return products;
        }

        
       
    }
}
