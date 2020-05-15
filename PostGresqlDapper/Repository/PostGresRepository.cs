using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using PostGresqlDapper.Model;
using PostGresqlDapper.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PostGresqlDapper.Repository
{
    public class PostGresRepository<T>: IPostGresRepository<T> where T : BaseModel
    {
        IConfiguration _config;
        private ILogger<PostGresRepository<T>> _logger;

        public PostGresRepository(ILogger<PostGresRepository<T>> logger, IConfiguration configuration)
        {
            _config = configuration;
            _logger = logger;
        }

        public static IDbConnection OpenConnection(string connStr)
        {
            var conn = new NpgsqlConnection(connStr);
            conn.Open();
            return conn;
        }

        public List<T> GetAll()
        {
            //var query = $"select * from {TableName} where id = {id}";
            //the data access layer is implemented elsewhere

            var appSettingsSection = _config.GetSection("ProjectSettings");
            var appSettings = appSettingsSection.Get<SettingsModel>();

            //string connString = "User ID=postgres;Password=password1234;Host=localhost;Port=5432;Database=dvdrental;";
            List<T> list;

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration["ConnectionString"];

            using (var conn = OpenConnection(connectionString))
            {
                var querySQL = @"SELECT actor_id, first_name, last_name, last_update FROM public.actor";
                list = conn.Query<T>(querySQL).ToList();
                return list;
            }
            
        }

        //ICollection<T> IPostGresRepository<T>.GetById()
        //{
        //    throw new NotImplementedException();
        //}

        //T IPostGresRepository<T>.GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
