using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PunchOutAPI;
using PunchOutAPI.Models;

namespace PunchOutAPI.Business
{
    public interface IBusinessManager
    {
        IEnumerable<URLInfo> GetAllURL();
        int PostAllURL(URLInfo issue);
    }
}