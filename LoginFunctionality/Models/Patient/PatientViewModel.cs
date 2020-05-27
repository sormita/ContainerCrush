using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginFunctionality.Models.Patient
{
    public class PatientViewModel
    {
        public List<AppointmentListDoctor> AvailableDoctors { get; set; }
        public int SelectedDoctor { get; set; }
        public DateTime AppointmentDate { get; set; } 
    }
}
