using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string Status { get; set; }
    }

    public class BookAppointment
    {        
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string Status { get; set; }
    }
}
