using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorService.Entities;
using DoctorService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DoctorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        IDoctorRepository _repos;
        ILogger<DoctorController> _logger;

        public DoctorController(IDoctorRepository irepos, ILogger<DoctorController> logger)
        {
            _repos = irepos;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllAppointments")]
        public List<DoctorDiary> GetAllAppointments(int DoctorId)
        {
            return _repos.GetAllAppointments(DoctorId);
        }

        [HttpPost]
        [Route("CreateDiaryEntry")]
        public void CreateDiaryEntry([FromBody]DoctorDiary objDoctorDiary)
        {
            _logger.LogInformation(objDoctorDiary.DoctorId.ToString());
            _logger.LogInformation(objDoctorDiary.HospitalId.ToString());
            _logger.LogInformation(objDoctorDiary.AppointedTime.ToString());

            _repos.CreateDiaryEntry(objDoctorDiary);
        }
    }
}