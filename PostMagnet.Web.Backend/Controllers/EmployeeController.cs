using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;
using PostMagnet.Web.Backend.Specifications;
using PostMagnet.Services.Internal;
using PostMagnet.Web.Backend.Models;
using PostMagnet.Web.Backend.Filters;
using PostMagnet.Web.Backend.Security;

using AutoMapper;
using Newtonsoft.Json;
using PostMagnet.Web.Backend.Helpers;

namespace PostMagnet.Web.Backend.Controllers
{
    public class EmployeeController : BaseController
    {
        //Services will be injected
        private IEmployeeServices _employeeServices;
        private IRoleServices _roleServices;
        private INotificationServices _notificationServices;
        private ILogServices _logServices;
        private IMessageServices _messageServices;

        public EmployeeController(IEmployeeServices employeeServices, IRoleServices roleServices,
            ILogServices logServices, INotificationServices notificationServices, IMessageServices messageServices)
        {
            _employeeServices = employeeServices;
            _roleServices = roleServices;
            _logServices = logServices;
            _notificationServices = notificationServices;
            _messageServices = messageServices;
        }

        // GET: Employee Login
        [HttpGet]
        [AllowAnonymous]
        [UnitOfWorkFilter]
        public ActionResult Login()
        {
            if (User != null)
                return RedirectToAction("index", "home");
            return View();
        }

        // POST: Employee Authentication Process
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        public JsonResult Authenticate(EmployeeLoginViewModel model)
        {
            // incremental delay to prevent brute force attacks
            int incrementalDelay;

            if (!ModelState.IsValid)
                return Json(new { success = false, redirectUrl = string.Empty });

            Employee employee =
                _employeeServices.Authenticate(new EmployeeAvailabilitySpecification(model.Username), model.Password);

            if (employee == null)
            {
                // Login failed consequences
                // Increase the delay on failed login attemps
                if (HttpContext.Application[Request.UserHostAddress] == null)
                    incrementalDelay = 1;
                else
                    incrementalDelay = (int)HttpContext.Application[Request.UserHostAddress] * 2;

                HttpContext.Application[Request.UserHostAddress] = incrementalDelay;

                _logServices.Create(new Log
                {
                    Content =
                        LogBuilderHelper.EmployeeLoginLog(model.Username, Request.UserHostAddress, false,
                            incrementalDelay),
                    Created = DateTime.Now
                });

                return Json(new { success = false, redirectUrl = string.Empty });
            }

            if (employee.IsActive)
            {
                EmployeePrincipalSerializeModel serializeModel =
                        Mapper.Map<Employee, EmployeePrincipalSerializeModel>(employee);

                Session[employee.Username] =
                    employee.Role.Permissions.Select(p => p.Controller + "," + p.Action).ToList();

                string employeeData = JsonConvert.SerializeObject(serializeModel);

                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, employee.Username, DateTime.Now,
                    DateTime.Now.AddHours(12), false, employeeData);

                string encryptTicket = FormsAuthentication.Encrypt(authTicket);

                HttpCookie employeeCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
                Response.Cookies.Add(employeeCookie);

                // Login success consequences
                // Reset incremental delay on successful login
                if (HttpContext.Application[Request.UserHostAddress] != null)
                    HttpContext.Application.Remove(Request.UserHostAddress);

                _logServices.Create(new Log
                {
                    Content =
                        LogBuilderHelper.EmployeeLoginLog(model.Username, Request.UserHostAddress, true,
                            0),
                    Created = DateTime.Now
                });

                return Json(new { success = true, redirectUrl = Url.Action("index", "home") });
            }

            return Json(new { success = false, redirectUrl = string.Empty });
        }

        [UnitOfWorkFilter]
        public ActionResult Logout()
        {
            _logServices.Create(new Log
            {
                Content = LogBuilderHelper.EmployeeLogOut(User.Username, Request.UserHostAddress),
                Created = DateTime.Now
            });

            FormsAuthentication.SignOut();
            Session.Abandon();

            // Clear authentication cookie
            HttpCookie unauthenticateEmployeeCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            unauthenticateEmployeeCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(unauthenticateEmployeeCookie);

            HttpCookie aspNetCookie = new HttpCookie("ASP.NET_SessionId", "");
            aspNetCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(aspNetCookie);

            return Redirect(FormsAuthentication.LoginUrl);
        }

        // GET: Employee List
        [HttpGet]
        [UnitOfWorkFilter]
        [ActionAuthorization(Privilege = "Employee,Members")]
        public ActionResult Members()
        {
            AllowedRoleListSpecification allowedRoleListSpecification;
            // Create Specification Filter
            if (User.EmployeeRole == "Administrator")
            {
                allowedRoleListSpecification = new AllowedRoleListSpecification(new List<EmployeePrivilege>
                {
                    EmployeePrivilege.Editor,
                    EmployeePrivilege.Contributor
                });
            }
            else
            {
                allowedRoleListSpecification = new AllowedRoleListSpecification(new List<EmployeePrivilege>
                {
                    EmployeePrivilege.Contributor
                });
            }
            
            List<RoleListViewModel> roleListViewModels =
                Mapper.Map<List<Role>, List<RoleListViewModel>>(
                    _roleServices.FindList(allowedRoleListSpecification).ToList());
            return View(roleListViewModels);
        }

        // POST: Get Employee List
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Employee,Members")]
        public JsonResult GetEmployees(EmployeeListViewFilterModel model)
        {
            List<Employee> employees;

            // Create specification filters
            EmployeeActiveListSpecification activeSpec =
                new EmployeeActiveListSpecification(model.FilterActiveSelectOption == "All"
                    ? (bool?)null : model.FilterActiveSelectOption == "Active");

            if (User.EmployeeRole == "Editor")
            {
                // Create own employee specification filter
                EmployeeListByCreatorSpecification creatorSpec = new EmployeeListByCreatorSpecification(User.Username);
                
                // Get all employees satisfied the specification filter
                employees = _employeeServices.FindList(activeSpec.And(creatorSpec)).ToList();
            }
            else
            {
                employees = _employeeServices.FindList(activeSpec).ToList();
            }

            // Convert all employees to viewModel
            List<EmployeeListViewModel> employeeViewModels =
                Mapper.Map<List<Employee>, List<EmployeeListViewModel>>(employees);

            employeeViewModels.ForEach(e => e.UpdateAccessibility = User.IsInPrivilege("Employee,UpdateAccessibility"));
            employeeViewModels.ForEach(e => e.ViewEmployeeProfile = User.IsInPrivilege("Employee,ViewProfile"));

            return Json(new { data = employeeViewModels });
        }

        // POST: Create new employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Employee,Create")]
        public JsonResult CreateEmployee(EmployeeCreationViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Form is not valid. Cannot create new employee!" });

            if (!_employeeServices.CheckAvailability(new EmployeeAvailabilitySpecification(model.Username)))
                return Json(new { success = false, message = "Username is not available. Please choose another username!" });

            // Get the chosen role
            Role chosenRole = _roleServices.GetById(model.RoleId);

            // Map the newly-created employee
            Employee newEmployee = Mapper.Map<EmployeeCreationViewModel, Employee>(model);

            newEmployee.Role = chosenRole;
            newEmployee.Password = HashingManager.CreateHash(model.Password);

            if (chosenRole.Name == "Contributor")
            {
                newEmployee.Rate = model.Rate;
            }

            // Get the current creator
            Employee creator = _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username));

            // Map the current creator to the new employee
            newEmployee.Creator = creator;

            _employeeServices.Create(newEmployee);

            _logServices.Create(new Log
            {
                Content =
                    LogBuilderHelper.EmployeeCreationLog(User.Username, newEmployee),
                Created = DateTime.Now
            });

            if (User.EmployeeRole == "Editor")
            {
                Employee currentSupervisor =
                    _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                _notificationServices.Create(new Notification
                {
                    Content =  NotificationBuilderHelper.EmployeeCreationNotification(User.Username, newEmployee),
                    Created = DateTime.Now,
                    IsRead = false,
                    Receiver = currentSupervisor,
                    Type = NotificationType.User
                });
            }

            return Json(new { success = true, message = "Successfully! New employee has been created!" });
        }

        // POST: Employee Allow/Disallow Accessibility
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Employee,UpdateAccessibility")]
        public JsonResult QuickUpdateAccessibility(EmployeeQuickUpdateAccessibilityViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Form is not valid. Cannot update employee's accessibility!" });

            Employee employeeUpdated = _employeeServices.FindBy(new EmployeeAvailabilitySpecification(model.Username));

            if (employeeUpdated == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            employeeUpdated.IsActive = !employeeUpdated.IsActive;

            _employeeServices.Update(employeeUpdated);

            _logServices.Create(new Log
            {
                Content =
                    LogBuilderHelper.EmployeeUpdateAccessibility(User.Username, employeeUpdated.Username,
                        employeeUpdated.IsActive),
                Created = DateTime.Now
            });

            if (User.EmployeeRole == "Editor")
            {
                Employee currentSupervisor =
                    _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                _notificationServices.Create(new Notification
                {
                    Content = NotificationBuilderHelper.EmployeeUpdateAccessibility(User.Username, employeeUpdated.Username, employeeUpdated.IsActive),
                    Created = DateTime.Now,
                    IsRead = false,
                    Receiver = currentSupervisor,
                    Type = NotificationType.User
                });
            }

            return Json(new { success = true, message = "Successfully! Employee's accessibility status has been changed!" });
        }

        // GET : Employee Profile
        [HttpGet]
        [UnitOfWorkFilter]
        [ActionAuthorization(Privilege = "Employee,ViewProfile")]
        public ActionResult ViewProfile(string username)
        {
            if (username == string.Empty)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            string employeeUsername = username;

            Employee employeeProfile = _employeeServices.FindBy(new EmployeeAvailabilitySpecification(employeeUsername));

            if (employeeProfile == null)
                return RedirectToAction("NotFound", "Error");

            EmployeeProfileViewModel profileViewModel = Mapper.Map<Employee, EmployeeProfileViewModel>(employeeProfile);            

            Tuple<EmployeeProfileViewModel, bool, int> viewModel =
                new Tuple<EmployeeProfileViewModel, bool, int>(profileViewModel,
                    employeeProfile.Role.Name == "Contributor", employeeProfile.Rate);

            return View(viewModel);
        }

        // POST: Employee Allow/Disallow Accessibility
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Employee,UpdateAccessibility")]
        public JsonResult UpdateAccessibility(EmployeeUpdateAccessibilityViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Form is not valid. Cannot update employee's accessibility!" });

            Employee employeeUpdated = _employeeServices.FindBy(new EmployeeAvailabilitySpecification(model.Username));

            if (employeeUpdated == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            employeeUpdated.IsActive = model.IsActive;

            _employeeServices.Update(employeeUpdated);

            _logServices.Create(new Log
            {
                Content =
                    LogBuilderHelper.EmployeeUpdateAccessibility(User.Username, employeeUpdated.Username,
                        employeeUpdated.IsActive),
                Created = DateTime.Now
            });

            if (User.EmployeeRole == "Editor")
            {
                Employee currentSupervisor =
                    _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                _notificationServices.Create(new Notification
                {
                    Content = NotificationBuilderHelper.EmployeeUpdateAccessibility(User.Username, employeeUpdated.Username, employeeUpdated.IsActive),
                    Created = DateTime.Now,
                    IsRead = false,
                    Receiver = currentSupervisor,
                    Type = NotificationType.User
                });
            }

            return Json(new { success = true, message = "Successfully! Employee's accessibility status has been changed!" });
        }

        // POST: Reset employee password
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Employee,ResetPassword")]
        public JsonResult ResetPassword(EmployeeResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Form is not valid. Cannot update employee password!" });

            Employee employeeUpdated = _employeeServices.FindBy(new EmployeeAvailabilitySpecification(model.Username));

            if (employeeUpdated == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            employeeUpdated.Password = HashingManager.CreateHash(model.NewPassword);

            _employeeServices.Update(employeeUpdated);

            _logServices.Create(new Log
            {
                Content =
                    LogBuilderHelper.EmployeeResetPassword(User.Username, employeeUpdated.Username),
                Created = DateTime.Now
            });

            if (User.EmployeeRole == "Editor")
            {
                Employee currentSupervisor =
                    _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                _notificationServices.Create(new Notification
                {
                    Content = NotificationBuilderHelper.EmployeeResetPassword(User.Username, employeeUpdated.Username),
                    Created = DateTime.Now,
                    IsRead = false,
                    Receiver = currentSupervisor,
                    Type = NotificationType.User
                });
            }

            return Json(new { success = true, message = "Successfully! Employee's password has been changed!" });
        }

        // POST: Update employee rate
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Employee,UpdateRate")]
        public JsonResult UpdateRate(EmployeeRateUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Form is not valid. Cannot update employee's rate!" });

            Employee employeeUpdated = _employeeServices.FindBy(new EmployeeAvailabilitySpecification(model.Username));

            if (employeeUpdated == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            int oldRate = employeeUpdated.Rate;

            employeeUpdated.Rate = model.Rate;

            _employeeServices.Update(employeeUpdated);

            _logServices.Create(new Log
            {
                Content =
                    LogBuilderHelper.EmployeeUpdateRate(User.Username, employeeUpdated.Username, oldRate, model.Rate),
                Created = DateTime.Now
            });

            if (User.EmployeeRole == "Editor")
            {
                Employee currentSupervisor =
                    _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                _notificationServices.Create(new Notification
                {
                    Content = NotificationBuilderHelper.EmployeeUpdateRate(User.Username, employeeUpdated.Username),
                    Created = DateTime.Now,
                    IsRead = false,
                    Receiver = currentSupervisor,
                    Type = NotificationType.User
                });
            }

            return Json(new { success = true, message = "Successfully! Employee's rate has been changed!" });
        }

        // GET: Personal Profile
        [HttpGet]
        [UnitOfWorkFilter]
        [ActionAuthorization(Privilege = "Employee,MyProfile")]
        public ActionResult MyProfile()
        {
            // Get general information
            EmployeeProfileViewModel profileViewModel = Mapper.Map<EmployeePrincipal, EmployeeProfileViewModel>(User);

            int rate = 0;
            // Check whether current user is a contributor
            if (User.EmployeeRole == "Contributor")
            {
                Employee currentEmployee = _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username));

                rate = currentEmployee.Rate;
            }

            Tuple<EmployeeProfileViewModel, bool, int> viewModel =
                new Tuple<EmployeeProfileViewModel, bool, int>(profileViewModel,
                    User.EmployeeRole == "Contributor", rate);

            return View(viewModel);
        }

        // POST: Change personal profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Employee,UpdateInfo")]
        public JsonResult UpdateInfo(EmployeeChangeProfileViewModel model)
        {
            Employee employeeUpdated = _employeeServices.GetById(User.EmployeeId);

            if (employeeUpdated == null)
            {
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });
            }

            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Form is not valid. Cannot update your profile!" });

            employeeUpdated = Mapper.Map(model, employeeUpdated);

            _employeeServices.Update(employeeUpdated);

            _logServices.Create(new Log
            {
                Content = LogBuilderHelper.PersonalUpdateProfileLog(employeeUpdated.Username, employeeUpdated),
                Created = DateTime.Now
            });

            return Json(new { success = true, message = "Successfully! Your profile has been changed!" });
        }

        // POST: Change personal password
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Employee,UpdatePassword")]
        public JsonResult UpdatePassword(EmployeeChangePasswordViewModel model)
        {
            Employee employeeUpdated = _employeeServices.GetById(User.EmployeeId);

            if (employeeUpdated == null)
            {
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });
            }

            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Form is not valid. Cannot update your password!" });

            if (!HashingManager.ValidatePassword(model.OldPassword, employeeUpdated.Password))
                return Json(new { success = false, message = "Password incorrect! Cannot update your password." });

            employeeUpdated.Password = HashingManager.CreateHash(model.NewPassword);

            _employeeServices.Update(employeeUpdated);

            _logServices.Create(new Log
            {
                Content = LogBuilderHelper.PersonalUpdatePasswordLog(employeeUpdated.Username),
                Created = DateTime.Now
            });

            return Json(new { success = true, message = "Successfully! Your password has been changed!" });
        }

        #region Notifications

        // GET: Get Current Pending Notifications
        [HttpGet]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        public JsonResult GetPendingNotifications()
        {
            // Create the specifications
            NotificationListByUsernameSpecification usernameSpecification = new NotificationListByUsernameSpecification(User.Username);

            NotificationPendingListSpecification pendingListSpecification = new NotificationPendingListSpecification();

            // Get numbers of pending notifications
            List<Notification> pendingNotifications =
                _notificationServices.FindList(usernameSpecification.And(pendingListSpecification)).ToList();

            pendingNotifications = pendingNotifications.OrderByDescending(n => n.Created).ToList();

            // Mapping to viewmodels
            List<NotificationMenuListViewModel> viewModels =
                Mapper.Map<List<Notification>, List<NotificationMenuListViewModel>>(pendingNotifications.Take(10).ToList());

            return Json(new { number = pendingNotifications.Count , list = viewModels},
                JsonRequestBehavior.AllowGet);
        }

        // POST: Get Current Notifications
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        public JsonResult GetNotifications()
        {
            // Create the specifications
            NotificationListByUsernameSpecification usernameSpecification = new NotificationListByUsernameSpecification(User.Username);

            // Get notifications list
            List<Notification> notifications = _notificationServices.FindList(usernameSpecification).ToList();

            notifications = notifications.OrderByDescending(n => n.Created).ToList();

            // Mapping to viewmodel
            List<NotificationListViewModel> viewModels =
                Mapper.Map<List<Notification>, List<NotificationListViewModel>>(notifications);

            return Json(new { data = viewModels });
        }

        // POST: Update Notification Status
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        public JsonResult MarkNotificationRead(int id)
        {
            var updateNotification = _notificationServices.GetById(id);

            if(updateNotification == null || updateNotification.Receiver.Username != User.Username)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            updateNotification.IsRead = true;

            _notificationServices.Update(updateNotification);

            NotificationListViewModel updateViewModel =
                Mapper.Map<Notification, NotificationListViewModel>(updateNotification);

            return Json(new { success = true, message = "Mark as read!", info = updateViewModel });
        }

        #endregion

        #region Messages

        // GET: Get Current Unread Messages
        [HttpGet]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        public JsonResult GetUnreadMessages()
        {
            // Create the specifications
            MessageListByUsernameSpecification usernameSpecification = new MessageListByUsernameSpecification(User.Username);

            MessageUnreadListSpecification unreadListSpecification = new MessageUnreadListSpecification();

            // Get numbers of unread messages
            int unreadMessages =
                _messageServices.FindList(usernameSpecification.And(unreadListSpecification)).Count();

            return Json(new { number = unreadMessages }, JsonRequestBehavior.AllowGet);
        }

        // POST: Get Current Messages
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        public JsonResult GetMessages()
        {
            // Create the specification filter
            MessageListByUsernameSpecification usernameSpecification = new MessageListByUsernameSpecification(User.Username);

            // Get list of messages
            List<Message> messages = _messageServices.FindList(usernameSpecification).ToList();

            messages = messages.OrderByDescending(m => m.Sent).ToList();

            // Mapping to viewModels
            List<MessageListViewModel> viewModels = Mapper.Map<List<Message>, List<MessageListViewModel>>(messages);

            return Json(new { data = viewModels });
        }

        // POST: Update Message Status
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        public JsonResult MarkMessageRead(string code)
        {
            // Create the specification
            MessageByCodeSpecification codeSpecification = new MessageByCodeSpecification(code);

            var updateMessage = _messageServices.FindBy(codeSpecification);

            if (updateMessage == null || updateMessage.Recipient.Username != User.Username)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            updateMessage.IsRead = true;

            _messageServices.Update(updateMessage);

            MessageListViewModel updateViewModel = Mapper.Map<Message, MessageListViewModel>(updateMessage);

            return Json(new { success = true, message = "Mark as read!", info = updateViewModel });
        }

        #endregion
    }
}