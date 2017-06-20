using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PunchOutAPI.Models
{
    public class LoginUsers
    {
        public int DeviceId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}