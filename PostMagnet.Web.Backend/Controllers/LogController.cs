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
    public class LogController : BaseController
    {
        //Services will be injected
        ILogServices _logServices;

        public LogController(ILogServices logServices)
        {
            _logServices = logServices;
        }

        // GET: Log List
        [HttpGet]
        [ActionAuthorization(Privilege = "Log,Index")]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Get Log List
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Log,Index")]
        public JsonResult GetLogs()
        {
            // Get all logs
            List<Log> logs = _logServices.List().ToList();

            // Convert all logs to viewModel
            List<LogListViewModel> logViewModels =
                Mapper.Map<List<Log>, List<LogListViewModel>>(logs);

            logViewModels = logViewModels.OrderByDescending(l => l.Created).ToList();

            return Json(new { data = logViewModels });
        }
    }
}