using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostGresqlDapper.Model
{
    public class Actor: BaseModel
    {
        public int actor_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
}
