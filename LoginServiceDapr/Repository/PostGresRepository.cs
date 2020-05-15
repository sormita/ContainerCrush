using Microsoft.Extensions.Configuration;
using Dapper;
using LoginServiceDapr.Model;
using LoginServiceDapr.Repository.Entity;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using LoginServiceDapr.Utility;
using System.IO;

namespace LoginServiceDapr.Repository
{
    public class PostGresRepository: IPostGresRepository
    {
        IConfiguration _config;
        //private ILogger _logger;

        public PostGresRepository(IConfiguration configuration)
        {
            _config = configuration;
            //_logger = logger;
        }

        IDbConnection OpenConnection(string connStr)
        {
            var conn = new NpgsqlConnection(connStr);
            conn.Open();
            return conn;
        }

        public Person GetLoginUser(string email, string password)
        {
            //string connString = "User ID=postgres;Password=password1234;Host=localhost;Port=5432;Database=dvdrental;";

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
                Person personUsr = conn.Query<Person>(@"SELECT ""PersonID"", ""PersonRoleId"", ""PersonName"", ""PersonEmail"" FROM public.""Person"" WHERE ""PersonEmail""=@email AND ""Password""=@usr_password;", new { email = email, usr_password = password },
                commandType: CommandType.Text).ToList().FirstOrDefault();
                //var querySQL = @"SELECT * FROM public.Person";
                //list = conn.Query<Person>(querySQL).ToList();
                return personUsr;
            }
        }
    }
}
