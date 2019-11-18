using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class MenuController : BaseController
    {
        // Controller variables
        private List<Permission> _systemPermissions;

        // Services will be injected
        IPermissionServices _permissionServices;

        public MenuController(IPermissionServices permissionServices)
        {
            _permissionServices = permissionServices;
            _systemPermissions = new List<Permission>();
        }

        // GET: Menu
        [ChildActionOnly]
        public ActionResult Main()
        {
            List<string> currentUserPermissions = (List<string>)Session[User.Username];

            _systemPermissions = _permissionServices.List().ToList();

            _systemPermissions =
                _systemPermissions.Where(sp => currentUserPermissions.Contains(sp.Controller + "," + sp.Action) && sp.IsMainMenu).ToList();

            List<PermissionMenuListViewModel> userPermissions =
                Mapper.Map<List<Permission>, List<PermissionMenuListViewModel>>(_systemPermissions);

            string selectedMenuId =
                ControllerContext.ParentActionViewContext.RouteData.Values["action"].ToString().ToLower() +
                "-" +
                ControllerContext.ParentActionViewContext.RouteData.Values["controller"].ToString().ToLower();

            Tuple<List<PermissionMenuListViewModel>, string> viewModel = new Tuple<List<PermissionMenuListViewModel>, string>(userPermissions, selectedMenuId);

            return PartialView(viewModel);
        }

        // GET: TopMenu
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            return PartialView();
        }

        // GET: Breadcrumb
        [ChildActionOnly]
        public ActionResult Breadcrumb()
        {
            string selectedMenuId =
                ControllerContext.ParentActionViewContext.RouteData.Values["action"].ToString().ToLower() +
                "-" +
                ControllerContext.ParentActionViewContext.RouteData.Values["controller"].ToString().ToLower();
            return PartialView();
        }

        // GET: Pageinformation
        [ChildActionOnly]
        public ActionResult PageInformation()
        {
            return PartialView();
        }
    }
}