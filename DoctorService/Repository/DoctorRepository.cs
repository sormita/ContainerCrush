using Dapper;
using DoctorService.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorService.Repository
{
    public class DoctorRepository: IDoctorRepository
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

        public List<DoctorDiary> GetAllAppointments(int DoctorId)
        {
            using (var conn = OpenConnection())
            {
                List<DoctorDiary> lstDoctorDiary = conn.Query<DoctorDiary>(@"SELECT * from public.""DoctorDiary"" where ""DoctorId""=@DoctorID;", new { DoctorID= DoctorId },
                commandType: CommandType.Text).ToList();
                return lstDoctorDiary;
            }
        }

        public void CreateDiaryEntry(DoctorDiary objDoctorDiary)
        {
            try
            {                
                using (var conn = OpenConnection())
                {
                    conn.Execute("CALL public.usp_createdocdiaryentry(@DoctorId,@HospitalId,@WorkType,@AppointedTime)",
                        new { DoctorId = objDoctorDiary.DoctorId, HospitalId = objDoctorDiary.HospitalId, WorkType = objDoctorDiary.WorkType, AppointedTime = objDoctorDiary.AppointedTime }, null, null);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
