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
using Microsoft.Extensions.Options;

namespace LoginServiceDapr.Repository
{
    public class PostGresRepository: IPostGresRepository
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

        public Person GetLoginUser(string email, string password)
        {
            using (var conn = OpenConnection())
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
