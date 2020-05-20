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

namespace ProductService.Repository
{
    public class ProductRepository: IProductRepository
    {
        IConfiguration _config;

        public ProductRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        IDbConnection OpenConnection(string connStr)
        {
            var conn = new NpgsqlConnection(connStr);
            conn.Open();
            return conn;
        }

        public List<Product> GetAllProducts()
        {
            var appSettingsSection = _config.GetSection("ProjectSettings");
            var appSettings = appSettingsSection.Get<SettingsModel>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration["ConnectionString"];

            using (var conn = OpenConnection(connectionString))
            {
                List<Product> lstProduct = conn.Query<Product>(@"SELECT * from public.""Medicine"";",
                commandType: CommandType.Text).ToList();                
                return lstProduct;
            }
        }
    }
}
