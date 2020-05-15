using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginFunctionality.Models
{
    public class Review: BaseModel
    {
        [JsonProperty(PropertyName = "reviewerName")]
        public string reviewerName { get; set; }
        public DateTime reviewDate { get; set; }
        public string Comments { get; set; }
        public string EmailAddress { get; set; }
    }
}
