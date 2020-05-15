using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginServiceDapr.Repository.Entity
{
    public class Person
    {
        public int PersonID { get; set; }
        public int PersonRoleId { get; set; }
        public string PersonName { get; set; }
        public string PersonEmail { get; set; }        
    }
}
