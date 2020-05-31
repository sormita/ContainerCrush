using Microsoft.Extensions.Configuration;
using Npgsql;
using PatientService.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Dapper;
using System.Threading.Tasks;

namespace PatientService.Repository
{
    public class AppointmentRepository : IAppointmentRepository
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

        public List<Appointment> GetAllAppointments()
        {
            using (var conn = OpenConnection())
            {
                List<Appointment> lstAppointmnt = conn.Query<Appointment>(@"SELECT * from public.""Appointment"";",
                commandType: CommandType.Text).ToList();
                return lstAppointmnt;
            }
        }

        public List<Doctor> GetDoctors()
        {
            using (var conn = OpenConnection())
            {
                List<Doctor> lstDoctor = conn.Query<Doctor>(@"SELECT * from public.""doctor_info"";",
                commandType: CommandType.Text).ToList();
                return lstDoctor;
            }
        }

        public void CreateAppointment(Appointment objAppointment)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("PatientId", objAppointment.PatientId, DbType.Int32, ParameterDirection.Input);
                param.Add("DoctorId", objAppointment.DoctorId, DbType.Int32, ParameterDirection.Input);
                param.Add("AppointmentTime", objAppointment.PatientId, DbType.Int32, ParameterDirection.Input);
                param.Add("AppointmentTime", objAppointment.AppointmentTime, DbType.DateTime, ParameterDirection.Input);
                using (var conn = OpenConnection())
                {
                    conn.Execute("CALL public.usp_createappointment(@PatientId,@DoctorId,@AppointmentTime,@Status)",
                        new { PatientId = objAppointment.PatientId, DoctorId = objAppointment.DoctorId, AppointmentTime= objAppointment.AppointmentTime,Status="Booked" }, null, null);
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }
    }
}
