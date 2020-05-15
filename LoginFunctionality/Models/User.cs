using Microsoft.AspNetCore.Identity;
using System;

namespace LoginFunctionality.Models
{
    public class User : IdentityUser
    {
        public int PersonID { get; set; }
        public int PersonRoleId { get; set; }
        public string PersonName { get; set; }
        public string PersonEmail { get; set; }
    }
}