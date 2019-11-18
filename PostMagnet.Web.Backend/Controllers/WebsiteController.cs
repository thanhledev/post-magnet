using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;

using PostMagnet.Domain.Entities;
using PostMagnet.Services.Internal;
using PostMagnet.Web.Backend.Models;
using PostMagnet.Web.Backend.Filters;
using PostMagnet.Web.Backend.Security;
using PostMagnet.Web.Backend.Helpers;

using AutoMapper;
using JoeBlogs;

namespace PostMagnet.Web.Backend.Controllers
{
    public class WebsiteController : BaseController
    {
        // Services will be injected
        IWebsiteServices _websiteServices;
        ILogServices _logServices;

        public WebsiteController(IWebsiteServices websiteServices, ILogServices logServices)
        {
            _websiteServices = websiteServices;
            _logServices = logServices;
        }

        // GET: Website List
        [HttpGet]
        [UnitOfWorkFilter]
        [ActionAuthorization(Privilege = "Website,Websites")]
        public ActionResult Websites()
        {
            ReadOnlyCollection<TimeZoneInfo> tz;
            tz = TimeZoneInfo.GetSystemTimeZones();

            List<TimeZoneListViewModel> viewModels =
                Mapper.Map<ReadOnlyCollection<TimeZoneInfo>, List<TimeZoneListViewModel>>(tz);

            return View(viewModels);
        }

        // POST: Get Websites List
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Website,Websites")]
        public JsonResult GetWebsites()
        {
            // Get all websites
            var websites = _websiteServices.List().ToList();

            // Convert all websites to viewModel
            List<WebsiteListViewModel> websiteViewModels =
                Mapper.Map<List<Website>, List<WebsiteListViewModel>>(websites);

            websiteViewModels.ForEach(e => e.Editable = User.IsInPrivilege("Website,Update"));
            websiteViewModels.ForEach(e => e.Testable = User.IsInPrivilege("Website,CheckConnectivity"));

            return Json(new { data = websiteViewModels });
        }

        // POST: Get Website Detail
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Website,Update")]
        public JsonResult WebsiteDetail(int? id)
        {
            if (id == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            int websiteId = id.Value;
            Website updateWebsite = _websiteServices.GetById(websiteId);

            if (updateWebsite == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            WebsiteDetailViewModel viewModel = Mapper.Map<Website, WebsiteDetailViewModel>(updateWebsite);

            return Json(new { success = true, info = viewModel });
        }

        // POST: Create New Website
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Website,Create")]
        public JsonResult Create(WebsiteCreationViewModel viewModel)
        {
            if(!ModelState.IsValid)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            var newWebsite = Mapper.Map<WebsiteCreationViewModel, Website>(viewModel);

            if (viewModel.RequireTesting)
            {
                try
                {
                    WordPressWrapper core = new WordPressWrapper(viewModel.Host + "/xmlrpc.php", viewModel.Username, viewModel.Password);

                    core.GetCategories();

                    newWebsite.Tested = DateTime.Now;
                    newWebsite.Note = "Test successfully!";
                }
                catch (Exception ex)
                {
                    newWebsite.Tested = DateTime.Now;
                    newWebsite.Note = "Test failed! Exception: " + ex.Message;
                }
            }

            _websiteServices.Create(newWebsite);

            //Write Log
            _logServices.Create(new Log
            {
                Created = DateTime.Now,
                Content = LogBuilderHelper.WebsiteCreationLog(User.Username, newWebsite)
            });

            return Json(new { success = true, message = "Successfully! New website has been created!" });
        }

        // POST: Update Website Information
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Website,Update")]
        public JsonResult Update(WebsiteUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            var updateWebsite = _websiteServices.GetById(viewModel.Id);

            if(updateWebsite == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            Mapper.Map(viewModel, updateWebsite);

            if (viewModel.RequireTesting)
            {
                try
                {
                    WordPressWrapper core = new WordPressWrapper(viewModel.Host + "/xmlrpc.php", viewModel.Username, viewModel.Password);

                    core.GetCategories();

                    updateWebsite.Tested = DateTime.Now;
                    updateWebsite.Note = "Test successfully!";
                }
                catch (Exception ex)
                {
                    updateWebsite.Tested = DateTime.Now;
                    updateWebsite.Note = "Test failed! Exception: " + ex.Message;
                }
            }

            _websiteServices.Update(updateWebsite);

            //Write Log
            _logServices.Create(new Log
            {
                Created = DateTime.Now,
                Content = LogBuilderHelper.WebsiteUpdateLog(User.Username, updateWebsite)
            });

            return Json(new { success = true, message = "Successfully! Website information has been updated!" });
        }

        // POST: Check Website's Connectivity
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Website,CheckConnectivity")]
        public JsonResult CheckWebsiteConnectivity(int id)
        {
            var checkWebsite = _websiteServices.GetById(id);

            if(checkWebsite == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            bool connectivity;

            try
            {
                WordPressWrapper core = new WordPressWrapper(checkWebsite.Host + "/xmlrpc.php", checkWebsite.Username, checkWebsite.Password);

                core.GetCategories();

                checkWebsite.Tested = DateTime.Now;
                checkWebsite.Note = "Test successfully!";
                connectivity = true;
            }
            catch (Exception ex)
            {
                checkWebsite.Tested = DateTime.Now;
                checkWebsite.Note = "Test failed! Exception: " + ex.Message;
                connectivity = false;
            }

            _websiteServices.Update(checkWebsite);

            return Json(new { success = connectivity, message = checkWebsite.Note });
        }
    }
}