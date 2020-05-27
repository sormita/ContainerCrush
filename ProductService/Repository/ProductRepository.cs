using Microsoft.Extensions.Configuration;
using Npgsql;
using ProductService.Entities;
using ProductService.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ProductService.Repository
{
    public class ProductRepository: IProductRepository
    {

        IDbConnection OpenConnection()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();

            string connectionString = configuration["ConnectionString"];

            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        public List<Product> GetAllProducts()
        {
            using (var conn = OpenConnection())
            {
                List<Product> lstProduct = conn.Query<Product>(@"SELECT * from public.""Medicine"";",
                commandType: CommandType.Text).ToList();                
                return lstProduct;
            }
        }
    }
}
