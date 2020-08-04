using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientService.Entities;
using PatientService.Repository;

namespace PatientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        IAppointmentRepository repos;
        ILogger<PatientController> _logger;

        public PatientController(IAppointmentRepository irepos,ILogger<PatientController> logger)
        {
            repos = irepos;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Appointment> GetAll()
        {
            return repos.GetAllAppointments();
        }

        [HttpGet]
        [Route("GetDoctors")]
        public IEnumerable<Doctor> GetDoctors()
        {
            return repos.GetDoctors();
        }

        [HttpPost]
        [Route("CreateAppointment")]
        public void CreateAppointment([FromBody]BookAppointment objAppointment)
        {
            _logger.LogInformation(objAppointment.DoctorId.ToString());
            _logger.LogInformation(objAppointment.PatientId.ToString());
            _logger.LogInformation(objAppointment.Status);

            repos.CreateAppointment(objAppointment);
        }
    }
}