using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginFunctionality.Models
{
    [Serializable]
    public class AppointmentListDoctor
    {
        public int PersonID { get; set; }
        public string PersonName { get; set; }
    }
}
