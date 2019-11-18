using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PostMagnet.Web.Backend.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Access Denined Error
        public ActionResult AccessDenied()
        {
            return View();
        }

        // GET: 404 Not Found Error
        public ActionResult NotFound()
        {
            return View();
        }
    }
}