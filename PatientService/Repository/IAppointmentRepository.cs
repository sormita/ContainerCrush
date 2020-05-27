using PatientService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Repository
{
    public interface IAppointmentRepository
    {
        List<Appointment> GetAllAppointments();
        List<Doctor> GetDoctors();
    }
}
