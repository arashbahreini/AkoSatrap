using AkoSatrap.AuthData;
using AkoSatrap.ErrorData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkoSatrap.Controllers
{

    [AuthAttribute]
    [ErrorAttribute]
    public class SecurityController : Controller
    {
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }

      
    }
}