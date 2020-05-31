using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorService.Entities
{
    public class DoctorDiary
    {
        public int DoctorId { get; set; }
        public int HospitalId { get; set; }
        public string WorkType { get; set; }
        public DateTime AppointedTime { get; set; }
    }
}
