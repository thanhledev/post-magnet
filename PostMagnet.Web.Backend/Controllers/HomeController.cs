using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;
using PostMagnet.Web.Backend.Specifications;
using PostMagnet.Services.Internal;
using PostMagnet.Web.Backend.Models;
using PostMagnet.Web.Backend.Filters;
using PostMagnet.Web.Backend.Security;

using AutoMapper;

namespace PostMagnet.Web.Backend.Controllers
{
    [ControllerAuthorization(Roles = "Administrator,Editor,Writer")]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}