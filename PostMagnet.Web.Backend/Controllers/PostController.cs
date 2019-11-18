using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Text;

using PostMagnet.Domain.Entities;
using PostMagnet.Services.Internal;
using PostMagnet.Web.Backend.Models;
using PostMagnet.Web.Backend.Filters;
using PostMagnet.Web.Backend.Security;
using PostMagnet.Web.Backend.Helpers;

using AutoMapper;
using JoeBlogs;
using HtmlAgilityPack;
using PostMagnet.Domain.SpecificationFramework;
using PostMagnet.Web.Backend.Specifications;
using PostStatus = PostMagnet.Domain.Entities.PostStatus;
using PostMagnet.Services.SeoServices;

namespace PostMagnet.Web.Backend.Controllers
{
    public class PostController : BaseController
    {
        // Services will be injected
        private IPostServices _postServices;
        private ILogServices _logServices;
        private IEmployeeServices _employeeServices;
        private IPostExtraPaymentServices _postExtraPaymentServices;
        private INotificationServices _notificationServices;
        private IWebsiteServices _websiteServices;

        public PostController(IPostServices postServices, ILogServices logServices, IEmployeeServices employeeServices,
            IPostExtraPaymentServices postExtraPaymentServices, INotificationServices notificationServices, IWebsiteServices websiteServices)
        {
            _postServices = postServices;
            _logServices = logServices;
            _employeeServices = employeeServices;
            _postExtraPaymentServices = postExtraPaymentServices;
            _notificationServices = notificationServices;
            _websiteServices = websiteServices;
        }

        // GET: Get All Posts
        [HttpGet]
        [ActionAuthorization(Privilege = "Post,Posts")]
        public ActionResult Posts()
        {
            return View();
        }

        // POST: Get All Posts With Administrator Privledges
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Post,AdministratorPosts")]
        public JsonResult AdministratorGetAllPosts(PostListViewFilterModel model)
        {
            if(!ModelState.IsValid)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            // Create specification filters
            PostStatusListSpecification postStatusListSpecification =
                new PostStatusListSpecification(model.FilterPostStatusOption == "All"
                    ? null
                    : (PostStatus?)
                        Enum.Parse(typeof (PostStatus), model.FilterPostStatusOption, true));

            PostListByUniquePercentageSpecification postListByUniquePercentageSpecification =
                new PostListByUniquePercentageSpecification(null, null);
            
            switch (model.FilterPostQualityOption)
            {
                case"All":
                    postListByUniquePercentageSpecification = new PostListByUniquePercentageSpecification(0, 100);
                    break;
                case "Good":
                    postListByUniquePercentageSpecification = new PostListByUniquePercentageSpecification(75,100);
                    break;
                case "Normal":
                    postListByUniquePercentageSpecification = new PostListByUniquePercentageSpecification(50,75);
                    break;
                case "Bad":
                    postListByUniquePercentageSpecification = new PostListByUniquePercentageSpecification(0, 50);
                    break;
            }

            // Get all posts which statisified the selected specifications
            List<Domain.Entities.Post> posts =
                _postServices.FindList(postStatusListSpecification.And(postListByUniquePercentageSpecification))
                    .ToList();

            // Sort the posts
            posts = posts.OrderByDescending(p => p.Created).ToList();

            // Convert all posts to viewModels
            List<AdminPostListViewModel> postListViewModels =
                Mapper.Map<List<Domain.Entities.Post>, List<AdminPostListViewModel>>(posts);

            // Set the available priviledges
            postListViewModels.ForEach(p =>
            {
                p.Viewable = true;
                p.Approvable = IsPostApprovable(User, p.Status);
                p.Deliverable = IsPostDeliverable(User, p.Status);
                p.Schedulable = IsPostSchedulable(User, p.Status);
                p.ChangeSchedulable = IsPostChangeSchedulable(User, p.Status);
                p.ViewExtraPayment = IsPostExtraPaymentViewable(User);
            });

            return Json(new { data = postListViewModels });
        }

        // POST: Get All Posts With Editor Privledges
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Post,EditorPosts")]
        public JsonResult EditorGetAllPosts(PostListViewFilterModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            // Create specification filters
            PostStatusListSpecification postStatusListSpecification =
                new PostStatusListSpecification(model.FilterPostStatusOption == "All"
                    ? null
                    : (PostStatus?)
                        Enum.Parse(typeof(PostStatus), model.FilterPostStatusOption, true));

            PostListByUniquePercentageSpecification postListByUniquePercentageSpecification =
                new PostListByUniquePercentageSpecification(null, null);

            switch (model.FilterPostQualityOption)
            {
                case "All":
                    postListByUniquePercentageSpecification = new PostListByUniquePercentageSpecification(0, 100);
                    break;
                case "Good":
                    postListByUniquePercentageSpecification = new PostListByUniquePercentageSpecification(75, 100);
                    break;
                case "Normal":
                    postListByUniquePercentageSpecification = new PostListByUniquePercentageSpecification(50, 75);
                    break;
                case "Bad":
                    postListByUniquePercentageSpecification = new PostListByUniquePercentageSpecification(0, 50);
                    break;
            }

            PostListByContributorSupervisorSpecification contributorSupervisorSpecification =
                new PostListByContributorSupervisorSpecification(User.EmployeeId);

            // Get all posts which statisified the selected specifications
            List<Domain.Entities.Post> posts =
                _postServices.FindList(
                    postStatusListSpecification.And(
                        postListByUniquePercentageSpecification.And(contributorSupervisorSpecification)))
                    .ToList();

            // Sort the posts
            posts = posts.OrderByDescending(p => p.Created).ToList();

            // Convert all posts to viewModels
            List<AdminPostListViewModel> postListViewModels =
                Mapper.Map<List<Domain.Entities.Post>, List<AdminPostListViewModel>>(posts);

            // Set the available priviledges
            postListViewModels.ForEach(p =>
            {
                p.Viewable = true;
                p.Approvable = IsPostApprovable(User, p.Status);
                p.Deliverable = IsPostDeliverable(User, p.Status);
                p.Schedulable = IsPostSchedulable(User, p.Status);
                p.ChangeSchedulable = IsPostChangeSchedulable(User, p.Status);
                p.ViewExtraPayment = IsPostExtraPaymentViewable(User);
            });

            return Json(new { data = postListViewModels });
        }

        // POST: Get All Posts With Contributor Privledges
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Post,ContributorPosts")]
        public JsonResult ContributorGetAllPosts(PostListViewFilterModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            // Create specification filters
            PostStatusListSpecification postStatusListSpecification =
                new PostStatusListSpecification(model.FilterPostStatusOption == "All"
                    ? null
                    : (PostStatus?)
                        Enum.Parse(typeof(PostStatus), model.FilterPostStatusOption, true));

            PostListByUniquePercentageSpecification postListByUniquePercentageSpecification =
                new PostListByUniquePercentageSpecification(null, null);

            switch (model.FilterPostQualityOption)
            {
                case "All":
                    postListByUniquePercentageSpecification = new PostListByUniquePercentageSpecification(0, 100);
                    break;
                case "Good":
                    postListByUniquePercentageSpecification = new PostListByUniquePercentageSpecification(75, 100);
                    break;
                case "Normal":
                    postListByUniquePercentageSpecification = new PostListByUniquePercentageSpecification(50, 75);
                    break;
                case "Bad":
                    postListByUniquePercentageSpecification = new PostListByUniquePercentageSpecification(0, 50);
                    break;
            }

            PostListByContributorSpecification contributorSpecification =
                new PostListByContributorSpecification(User.EmployeeId);

            // Get all posts which statisified the selected specifications
            List<Domain.Entities.Post> posts =
                _postServices.FindList(
                    postStatusListSpecification.And(
                        postListByUniquePercentageSpecification.And(contributorSpecification)))
                    .ToList();

            // Sort the posts
            posts = posts.OrderByDescending(p => p.Created).ToList();

            // Convert all posts to viewModels
            List<ContributorPostListViewModel> postListViewModels =
                Mapper.Map<List<Domain.Entities.Post>, List<ContributorPostListViewModel>>(posts);

            // Set the available priviledges
            postListViewModels.ForEach(p =>
            {
                p.Viewable = true;
                p.Editable = IsPostEditable(User, p.Status);
                p.Removable = IsPostRemovable(User, p.Status);
                p.Submitable = IsPostSubmitable(User, p.Status);
                p.ViewExtraPayment = IsPostExtraPaymentViewable(User);
            });

            return Json(new { data = postListViewModels });
        }

        // POST: Quick Approve Post
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Post,Approve")]
        public JsonResult QuickApprovePost(QuickApprovePostViewModel viewModel)
        {
            if(!ModelState.IsValid)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            Domain.Entities.Post selectedPost = _postServices.FindBy(new PostByCodeSpecification(viewModel.Code));

            if(selectedPost == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            if (GenericHelper.ParseEnum<PostStatus>(viewModel.Confirmation) == PostStatus.Approved)
            {
                if (string.Compare(viewModel.Submission, "No", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    selectedPost.Status = PostStatus.Approved;
                    selectedPost.Approved = DateTime.Now;

                    _postServices.Update(selectedPost);

                    // Create log
                    _logServices.Create(new Log
                    {
                        Created = DateTime.Now,
                        Content =
                            LogBuilderHelper.ApprovePostLog(User.Username, viewModel.Code, true, viewModel.Submission,
                                string.Empty, string.Empty, string.Empty)
                    });

                    // Create notification
                    Employee currentAuthor =
                        _employeeServices.FindBy(new EmployeeAvailabilitySpecification(selectedPost.Contributor.Username));

                    _notificationServices.Create(new Notification
                    {
                        Content = NotificationBuilderHelper.ApprovePostNotification(User.Username, viewModel.Code, true),
                        Created = DateTime.Now,
                        IsRead = false,
                        Receiver = currentAuthor,
                        Type = NotificationType.Post
                    });

                    if (User.EmployeeRole == "Editor")
                    {
                        // Create supervisor's notification
                        Employee currentSupervisor =
                            _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                        _notificationServices.Create(new Notification
                        {
                            Content = NotificationBuilderHelper.ApprovePostNotification(User.Username, viewModel.Code, true),
                            Created = DateTime.Now,
                            IsRead = false,
                            Receiver = currentSupervisor,
                            Type = NotificationType.Post
                        });
                    }

                    // Return the post to update table
                    return
                        Json(
                            new
                            {
                                success = true,
                                message = "Approve successfully!",
                                post = ReturnAdminPostListViewModelToTable(selectedPost)
                            });
                }
                if (string.Compare(viewModel.Submission, "Yes", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    // get chosen website
                    Website selectedWebsite = _websiteServices.FindBy(new WebsiteByHostSpecification(viewModel.Host));

                    if (selectedWebsite == null)
                        return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

                    selectedPost.Status = PostStatus.Approved;
                    selectedPost.Approved = DateTime.Now;

                    bool isPosted;
                    string postedUrl = string.Empty;
                    string errorMessage = string.Empty;
                    try
                    {
                        WordPressWrapper core = new WordPressWrapper(viewModel.Host + "/xmlrpc.php", selectedWebsite.Username, selectedWebsite.Password);

                        var categories = core.GetCategories();

                        var selectedCategory = categories.FirstOrDefault(c => c.Name == viewModel.Category);

                        if (selectedCategory == null)
                        {
                            isPosted = false;
                            errorMessage = "Cannot find selected category";
                        }
                        else
                        {
                            var wordpressPost = CreateWordpressPost(selectedPost, selectedWebsite, selectedCategory);
                            postedUrl = core.NewPostPermalink(wordpressPost, true);
                            isPosted = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        isPosted = false;
                        errorMessage = ex.Message;
                    }

                    if (isPosted)
                    {
                        selectedPost.Status = PostStatus.Posted;
                        selectedPost.Posted = DateTime.Now;
                        selectedPost.Url = postedUrl;

                        _postServices.Update(selectedPost);

                        // Create log
                        _logServices.Create(new Log
                        {
                            Created = DateTime.Now,
                            Content =
                                LogBuilderHelper.ApprovePostLog(User.Username, viewModel.Code, true,
                                    viewModel.Submission,
                                    viewModel.Host, viewModel.Category, string.Empty)
                        });

                        // Create notification
                        Employee currentAuthor =
                            _employeeServices.FindBy(
                                new EmployeeAvailabilitySpecification(selectedPost.Contributor.Username));

                        _notificationServices.Create(new Notification
                        {
                            Content = NotificationBuilderHelper.ApproveAndDeliveryPostNotification(User.Username, viewModel.Code),
                            Created = DateTime.Now,
                            IsRead = false,
                            Receiver = currentAuthor,
                            Type = NotificationType.Post
                        });

                        if (User.EmployeeRole == "Editor")
                        {
                            // Create supervisor's notification
                            Employee currentSupervisor =
                                _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                            _notificationServices.Create(new Notification
                            {
                                Content = NotificationBuilderHelper.ApproveAndDeliveryPostNotification(User.Username, viewModel.Code),
                                Created = DateTime.Now,
                                IsRead = false,
                                Receiver = currentSupervisor,
                                Type = NotificationType.Post
                            });
                        }

                        // Return the post to update table
                        return
                            Json(
                                new
                                {
                                    success = true,
                                    message = "Posted Successfully!",
                                    post = ReturnAdminPostListViewModelToTable(selectedPost)
                                });
                    }
                    else
                    {
                        _postServices.Update(selectedPost);

                        // Create log 
                        _logServices.Create(new Log
                        {
                            Created = DateTime.Now,
                            Content =
                                LogBuilderHelper.ApprovePostLog(User.Username, viewModel.Code, true,
                                    viewModel.Submission,
                                    viewModel.Host, viewModel.Category, string.Empty)
                        });

                        // Create notification
                        Employee currentAuthor =
                            _employeeServices.FindBy(
                                new EmployeeAvailabilitySpecification(selectedPost.Contributor.Username));

                        _notificationServices.Create(new Notification
                        {
                            Content = NotificationBuilderHelper.ApprovePostNotification(User.Username, viewModel.Code, true),
                            Created = DateTime.Now,
                            IsRead = false,
                            Receiver = currentAuthor,
                            Type = NotificationType.Post
                        });

                        if (User.EmployeeRole == "Editor")
                        {
                            // Create supervisor's notification
                            Employee currentSupervisor =
                                _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                            _notificationServices.Create(new Notification
                            {
                                Content = NotificationBuilderHelper.ApprovePostNotification(User.Username, viewModel.Code, true),
                                Created = DateTime.Now,
                                IsRead = false,
                                Receiver = currentSupervisor,
                                Type = NotificationType.Post
                            });
                        }

                        // Return the post to update table
                        return
                            Json(
                                new
                                {
                                    success = false,
                                    message = errorMessage,
                                    post = ReturnAdminPostListViewModelToTable(selectedPost)
                                });
                    }
                }
                else
                {
                    selectedPost.Status = PostStatus.Scheduled;
                    selectedPost.Approved = DateTime.Now;

                    selectedPost.Scheduled = DateTime.ParseExact(viewModel.Scheduled, "MMMM dd yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture);

                    // Get selected website
                    var selectedWebsite = _websiteServices.FindBy(new WebsiteByHostSpecification(viewModel.Host));

                    if (selectedWebsite == null)
                        return
                            Json(new {success = false, message = "Error occurred! Please contact our administrators."});

                    selectedPost.ScheduledWebsite = selectedWebsite.Host;
                    selectedPost.ScheduledCategory = viewModel.Category;

                    _postServices.Update(selectedPost);

                    // Create log
                    _logServices.Create(new Log
                    {
                        Created = DateTime.Now,
                        Content =
                            LogBuilderHelper.ApprovePostLog(User.Username, viewModel.Code, true, viewModel.Submission,
                                selectedWebsite.Host, viewModel.Category, viewModel.Scheduled)
                    });

                    // Create notification
                    Employee currentAuthor =
                        _employeeServices.FindBy(new EmployeeAvailabilitySpecification(selectedPost.Contributor.Username));

                    _notificationServices.Create(new Notification
                    {
                        Content = NotificationBuilderHelper.ApprovePostNotification(User.Username, viewModel.Code, true),
                        Created = DateTime.Now,
                        IsRead = false,
                        Receiver = currentAuthor,
                        Type = NotificationType.Post
                    });

                    if (User.EmployeeRole == "Editor")
                    {
                        // Create supervisor's notification
                        Employee currentSupervisor =
                            _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                        _notificationServices.Create(new Notification
                        {
                            Content = NotificationBuilderHelper.ApprovePostNotification(User.Username, viewModel.Code, true),
                            Created = DateTime.Now,
                            IsRead = false,
                            Receiver = currentSupervisor,
                            Type = NotificationType.Post
                        });
                    }

                    // Return the post to update table
                    return
                        Json(
                            new
                            {
                                success = true,
                                message = "Scheduled Successfully!",
                                post = ReturnAdminPostListViewModelToTable(selectedPost)
                            });
                }
            }
            else
            {
                selectedPost.Status = PostStatus.Denied;
                selectedPost.Approved = DateTime.Now;

                _postServices.Update(selectedPost);

                // Create log
                _logServices.Create(new Log
                {
                    Created = DateTime.Now,
                    Content =
                        LogBuilderHelper.ApprovePostLog(User.Username, viewModel.Code, false, viewModel.Submission,
                            string.Empty, string.Empty, string.Empty)
                });

                // Create notification
                Employee currentAuthor =
                        _employeeServices.FindBy(new EmployeeAvailabilitySpecification(selectedPost.Contributor.Username));

                _notificationServices.Create(new Notification
                {
                    Content = NotificationBuilderHelper.ApprovePostNotification(User.Username, viewModel.Code, false),
                    Created = DateTime.Now,
                    IsRead = false,
                    Receiver = currentAuthor,
                    Type = NotificationType.Post
                });

                if (User.EmployeeRole == "Editor")
                {
                    // Create supervisor's notification
                    Employee currentSupervisor =
                        _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                    _notificationServices.Create(new Notification
                    {
                        Content = NotificationBuilderHelper.ApprovePostNotification(User.Username, viewModel.Code, false),
                        Created = DateTime.Now,
                        IsRead = false,
                        Receiver = currentSupervisor,
                        Type = NotificationType.Post
                    });
                }

                // Return the post to update table
                return
                    Json(
                        new
                        {
                            success = true,
                            message = "Denied Successfully!",
                            post = ReturnAdminPostListViewModelToTable(selectedPost)
                        });
            }
        }

        // POST: Quick Delivery Post
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Post,Delivery")]
        public JsonResult QuickDeliveryPost(QuickDeliveryPostViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            Domain.Entities.Post selectedPost = _postServices.FindBy(new PostByCodeSpecification(viewModel.Code));

            if (selectedPost == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            // get chosen website
            Website selectedWebsite = _websiteServices.FindBy(new WebsiteByHostSpecification(viewModel.Host));

            if (selectedWebsite == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            if (string.Compare(viewModel.Submission, "Yes", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                bool isPosted;
                string postedUrl = string.Empty;
                string errorMessage = string.Empty;
                try
                {
                    WordPressWrapper core = new WordPressWrapper(viewModel.Host + "/xmlrpc.php", selectedWebsite.Username, selectedWebsite.Password);

                    var categories = core.GetCategories();

                    var selectedCategory = categories.FirstOrDefault(c => c.Name == viewModel.Category);

                    if (selectedCategory == null)
                    {
                        isPosted = false;
                        errorMessage = "Cannot find selected category";
                    }
                    else
                    {
                        var wordpressPost = CreateWordpressPost(selectedPost, selectedWebsite, selectedCategory);
                        postedUrl = core.NewPostPermalink(wordpressPost, true);
                        isPosted = true;
                    }
                }
                catch (Exception ex)
                {
                    isPosted = false;
                    errorMessage = ex.Message;
                }

                if (isPosted)
                {
                    selectedPost.Status = PostStatus.Posted;
                    selectedPost.Posted = DateTime.Now;
                    selectedPost.Url = postedUrl;

                    _postServices.Update(selectedPost);

                    // Create log
                    _logServices.Create(new Log
                    {
                        Created = DateTime.Now,
                        Content =
                            LogBuilderHelper.ApprovePostLog(User.Username, viewModel.Code, true, viewModel.Submission,
                                viewModel.Host, viewModel.Category, string.Empty)
                    });

                    // Create notification
                    Employee currentAuthor =
                        _employeeServices.FindBy(new EmployeeAvailabilitySpecification(selectedPost.Contributor.Username));

                    _notificationServices.Create(new Notification
                    {
                        Content = NotificationBuilderHelper.ApproveAndDeliveryPostNotification(User.Username, viewModel.Code),
                        Created = DateTime.Now,
                        IsRead = false,
                        Receiver = currentAuthor,
                        Type = NotificationType.Post
                    });

                    if (User.EmployeeRole == "Editor")
                    {
                        // Create supervisor's notification
                        Employee currentSupervisor =
                            _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                        _notificationServices.Create(new Notification
                        {
                            Content = NotificationBuilderHelper.ApproveAndDeliveryPostNotification(User.Username, viewModel.Code),
                            Created = DateTime.Now,
                            IsRead = false,
                            Receiver = currentSupervisor,
                            Type = NotificationType.Post
                        });
                    }

                    // Return the post to update table
                    return
                        Json(
                            new
                            {
                                success = true,
                                message = "Posted Successfully!",
                                post = ReturnAdminPostListViewModelToTable(selectedPost)
                            });
                }

                return Json(new { success = false, message = errorMessage });
            }
            if (string.Compare(viewModel.Submission, "Schedule", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                selectedPost.Status = PostStatus.Scheduled;
                selectedPost.Scheduled = DateTime.ParseExact(viewModel.Scheduled, "MMMM dd yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture);
                selectedPost.ScheduledWebsite = selectedWebsite.Host;
                selectedPost.ScheduledCategory = viewModel.Category;

                _postServices.Update(selectedPost);

                // Create log
                _logServices.Create(new Log
                {
                    Created = DateTime.Now,
                    Content =
                        LogBuilderHelper.ApprovePostLog(User.Username, viewModel.Code, true, viewModel.Submission,
                            selectedWebsite.Host, viewModel.Category, viewModel.Scheduled)
                });

                // Create notifications
                Employee currentAuthor =
                    _employeeServices.FindBy(new EmployeeAvailabilitySpecification(selectedPost.Contributor.Username));

                _notificationServices.Create(new Notification
                {
                    Content = NotificationBuilderHelper.ApprovePostNotification(User.Username, viewModel.Code, true),
                    Created = DateTime.Now,
                    IsRead = false,
                    Receiver = currentAuthor,
                    Type = NotificationType.Post
                });

                if (User.EmployeeRole == "Editor")
                {
                    // Create supervisor's notification
                    Employee currentSupervisor =
                        _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                    _notificationServices.Create(new Notification
                    {
                        Content = NotificationBuilderHelper.ApprovePostNotification(User.Username, viewModel.Code, true),
                        Created = DateTime.Now,
                        IsRead = false,
                        Receiver = currentSupervisor,
                        Type = NotificationType.Post
                    });
                }

                // Return the post to update table
                return
                    Json(
                        new
                        {
                            success = true,
                            message = "Scheduled Successfully!",
                            post = ReturnAdminPostListViewModelToTable(selectedPost)
                        });
            }

            return Json(new { success = false, message = "Did nothing!" });
        }

        // POST: Quick Change Schedule Post
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Post,ChangeSchedule")]
        public JsonResult QuickSchedulePost(QuickSchedulePostViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            Domain.Entities.Post selectedPost = _postServices.FindBy(new PostByCodeSpecification(viewModel.Code));

            if (selectedPost == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            // Get selected website
            var selectedWebsite = _websiteServices.FindBy(new WebsiteByHostSpecification(viewModel.Host));

            if (selectedWebsite == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            var currentScheduleDate = selectedPost.Scheduled;
            var newScheduleDate = DateTime.ParseExact(viewModel.Scheduled, "MMMM dd yyyy HH:mm:ss",
                CultureInfo.InvariantCulture);

            // Case 1: Scheduled => Stop post
            if (string.Compare(viewModel.Submission, "No", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                selectedPost.Status = PostStatus.Approved;
                selectedPost.Scheduled = DateTime.MinValue;
                selectedPost.ScheduledWebsite = null;
                selectedPost.ScheduledCategory = null;

                _postServices.Update(selectedPost);

                // Create log
                _logServices.Create(new Log
                {
                    Content =
                        LogBuilderHelper.ChangeSchedulePostLog(User.Username, viewModel.Code, viewModel.Submission,
                            string.Empty, string.Empty, string.Empty),
                    Created = DateTime.Now
                });

                // Create notifications
                Employee currentAuthor =
                    _employeeServices.FindBy(new EmployeeAvailabilitySpecification(selectedPost.Contributor.Username));

                _notificationServices.Create(new Notification
                {
                    Content = NotificationBuilderHelper.UpdateSchedulePostNotification(User.Username, viewModel.Code, viewModel.Submission),
                    Created = DateTime.Now,
                    IsRead = false,
                    Receiver = currentAuthor,
                    Type = NotificationType.Post
                });

                if (User.EmployeeRole == "Editor")
                {
                    // Create supervisor's notification
                    Employee currentSupervisor =
                        _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                    _notificationServices.Create(new Notification
                    {
                        Content = NotificationBuilderHelper.UpdateSchedulePostNotification(User.Username, viewModel.Code, viewModel.Submission),
                        Created = DateTime.Now,
                        IsRead = false,
                        Receiver = currentSupervisor,
                        Type = NotificationType.Post
                    });
                }

                // Return the post to update table
                return
                    Json(
                        new
                        {
                            success = true,
                            message = "Update Successfully!",
                            post = ReturnAdminPostListViewModelToTable(selectedPost)
                        });
            }

            // Case 2: Scheduled => Submit now
            if (string.Compare(viewModel.Submission, "Yes", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                bool isPosted;
                string postedUrl = string.Empty;
                string errorMessage = string.Empty;
                try
                {
                    WordPressWrapper core = new WordPressWrapper(viewModel.Host + "/xmlrpc.php", selectedWebsite.Username, selectedWebsite.Password);

                    var categories = core.GetCategories();

                    var selectedCategory = categories.FirstOrDefault(c => c.Name == viewModel.Category);

                    if (selectedCategory == null)
                    {
                        isPosted = false;
                        errorMessage = "Cannot find selected category";
                    }
                    else
                    {
                        var wordpressPost = CreateWordpressPost(selectedPost, selectedWebsite, selectedCategory);
                        postedUrl = core.NewPostPermalink(wordpressPost, true);
                        isPosted = true;
                    }
                }
                catch (Exception ex)
                {
                    isPosted = false;
                    errorMessage = ex.Message;
                }

                if (isPosted)
                {
                    selectedPost.Status = PostStatus.Posted;
                    selectedPost.Posted = DateTime.Now;
                    selectedPost.Url = postedUrl;

                    _postServices.Update(selectedPost);

                    // Create log
                    _logServices.Create(new Log
                    {
                        Content =
                            LogBuilderHelper.ChangeSchedulePostLog(User.Username, viewModel.Code, viewModel.Submission,
                                viewModel.Host, viewModel.Category, string.Empty),
                        Created = DateTime.Now
                    });

                    // Create notification
                    Employee currentAuthor =
                        _employeeServices.FindBy(new EmployeeAvailabilitySpecification(selectedPost.Contributor.Username));

                    _notificationServices.Create(new Notification
                    {
                        Content =
                            NotificationBuilderHelper.UpdateSchedulePostNotification(User.Username, viewModel.Code,
                                viewModel.Submission),
                        Created = DateTime.Now,
                        IsRead = false,
                        Receiver = currentAuthor,
                        Type = NotificationType.Post
                    });

                    if (User.EmployeeRole == "Editor")
                    {
                        // Create supervisor's notification
                        Employee currentSupervisor =
                            _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                        _notificationServices.Create(new Notification
                        {
                            Content =
                                NotificationBuilderHelper.UpdateSchedulePostNotification(User.Username, viewModel.Code,
                                    viewModel.Submission),
                            Created = DateTime.Now,
                            IsRead = false,
                            Receiver = currentSupervisor,
                            Type = NotificationType.Post
                        });
                    }

                    // Return the post to update table
                    return
                        Json(
                            new
                            {
                                success = true,
                                message = "Posted Successfully!",
                                post = ReturnAdminPostListViewModelToTable(selectedPost)
                            });
                }

                return Json(new { success = false, message = errorMessage });
            }

            // Case 3: Change Scheduled Date or Host or Category
            if (string.Compare(viewModel.Submission, "Schedule", StringComparison.InvariantCultureIgnoreCase) == 0 &&
                (currentScheduleDate != newScheduleDate ||
                 string.Compare(selectedPost.ScheduledWebsite, viewModel.Host,
                     StringComparison.InvariantCultureIgnoreCase) != 0 ||
                 string.Compare(selectedPost.ScheduledCategory, viewModel.Category,
                     StringComparison.InvariantCultureIgnoreCase) != 0))
            {
                selectedPost.ScheduledWebsite = selectedWebsite.Host;
                selectedPost.ScheduledCategory = viewModel.Category;
                selectedPost.Scheduled = DateTime.ParseExact(viewModel.Scheduled, "MMMM dd yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture);
                _postServices.Update(selectedPost);

                // Create log
                _logServices.Create(new Log
                {
                    Content =
                        LogBuilderHelper.ChangeSchedulePostLog(User.Username, viewModel.Code, viewModel.Submission,
                            viewModel.Host, viewModel.Category, viewModel.Scheduled),
                    Created = DateTime.Now
                });

                // Create notification
                Employee currentAuthor =
                    _employeeServices.FindBy(new EmployeeAvailabilitySpecification(selectedPost.Contributor.Username));

                _notificationServices.Create(new Notification
                {
                    Content =
                        NotificationBuilderHelper.UpdateSchedulePostNotification(User.Username, viewModel.Code,
                            viewModel.Submission),
                    Created = DateTime.Now,
                    IsRead = false,
                    Receiver = currentAuthor,
                    Type = NotificationType.Post
                });

                if (User.EmployeeRole == "Editor")
                {
                    // Create supervisor's notification
                    Employee currentSupervisor =
                        _employeeServices.FindBy(new EmployeeAvailabilitySpecification(User.Username)).Creator;

                    _notificationServices.Create(new Notification
                    {
                        Content =
                            NotificationBuilderHelper.UpdateSchedulePostNotification(User.Username, viewModel.Code,
                                viewModel.Submission),
                        Created = DateTime.Now,
                        IsRead = false,
                        Receiver = currentSupervisor,
                        Type = NotificationType.Post
                    });
                }

                // Return the post to update table
                return
                    Json(
                        new
                        {
                            success = true,
                            message = "Update schedule successfully!",
                            post = ReturnAdminPostListViewModelToTable(selectedPost)
                        });
            }

            return Json(new { success = false, message = "Did nothing!" });
        }

        // POST: Quick Get Post's Schedule Information
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Post,Schedule")]
        public JsonResult GetPostScheduleInformation(string code)
        {
            Domain.Entities.Post selectedPost = _postServices.FindBy(new PostByCodeSpecification(code));

            if (selectedPost == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            QuickPostScheduleInformationViewModel viewModel =
                Mapper.Map<Domain.Entities.Post, QuickPostScheduleInformationViewModel>(selectedPost);

            return Json(new {success = true, post = viewModel});
        }

        #region Extra Payment

        // POST: Get Post's Extra Payment List
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Post,ViewExtraPayments")]
        public JsonResult PostExtraPaymentList(string code)
        {
            if (code == string.Empty)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            // Create specification filter
            PostByCodeSpecification postByCodeSpecification = new PostByCodeSpecification(code);

            Domain.Entities.Post post = _postServices.FindBy(postByCodeSpecification);

            if (post == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            // Create specification filter
            ExtraPaymentListByPostSpecification extrapaymentSpecification = new ExtraPaymentListByPostSpecification(code);

            // Get all extra payments satisfied the specification
            List<PostExtraPayment> extraPayments =
                _postExtraPaymentServices.FindList(extrapaymentSpecification).ToList();

            // Convert extra payments to viewModels
            List<PostExtraPaymentListViewModel> extraPaymentListViewModels =
                Mapper.Map<List<PostExtraPayment>, List<PostExtraPaymentListViewModel>>(extraPayments);

            return Json(new { success = true, payments = extraPaymentListViewModels });
        }

        // POST: Create new extra payment
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Post,AddExtraPayment")]
        public JsonResult AddPostExtraPayment(PostExtraPaymentCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Form is not valid. Cannot add new extra payment!" });

            Domain.Entities.Post updatePost = _postServices.GetById(model.PostId);

            if (updatePost == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            PostExtraPayment newExtraPayment = Mapper.Map<PostExtraPaymentCreateViewModel, PostExtraPayment>(model);
            newExtraPayment.Post = updatePost;
            updatePost.ExtraPayments.Add(newExtraPayment);

            _postServices.Update(updatePost);

            return Json(new { success = true, message = "Successfully! New extra payment has been created and add to the associated post!" });
        }

        // POST: Get extra payment information
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Post,UpdateExtraPayment")]
        public JsonResult GetPostExtraPayment(PostExtraPaymentDetailViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Form is not valid. Cannot view extra payment information!" });

            Domain.Entities.Post post = _postServices.GetById(model.PostId);

            if (post == null || post.ExtraPayments.All(e => e.Id != model.Id))
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            // Get extra payment detail
            PostExtraPayment paymentDetail = post.ExtraPayments.First(e => e.Id == model.Id);

            // Convert extra payment to viewModel
            PostExtraPaymentListViewModel viewModel =
                Mapper.Map<PostExtraPayment, PostExtraPaymentListViewModel>(paymentDetail);

            return Json(new { success = true, payment = viewModel });
        }

        // POST: Update extra payment information
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Post,UpdateExtraPayment")]
        public JsonResult UpdatePostExtraPayment(PostExtraPaymentUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Form is not valid. Cannot update extra payment!" });

            Domain.Entities.Post updatePost = _postServices.GetById(model.PostId);

            if (updatePost == null || updatePost.ExtraPayments.All(e => e.Id != model.Id))
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            Mapper.Map(model, updatePost.ExtraPayments.First(e => e.Id == model.Id));

            _postServices.Update(updatePost);

            return Json(new { success = true, message = "Successfully! Extra payment has been updated!" });
        }

        // POST: Remove extra payment
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Post,RemoveExtraPayment")]
        public JsonResult RemovePostExtraPayment(PostExtraPaymentDeletionViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Form is not valid. Cannot remove extra payment!" });

            Domain.Entities.Post updatePost = _postServices.GetById(model.PostId);

            if (updatePost == null || updatePost.ExtraPayments.All(e => e.Id != model.Id))
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            updatePost.ExtraPayments.Remove(updatePost.ExtraPayments.First(e => e.Id == model.Id));

            _postServices.Update(updatePost);

            return Json(new { success = true, message = "Successfully! Extra payment has been removed!" });
        }

        #endregion

        #region Website

        // GET: Get All Available Websites
        [HttpGet]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Website,AvailableWebsites")]
        public JsonResult GetAvailableWesites()
        {
            // Get all websites
            var websites = _websiteServices.List().ToList();

            // Convert all websites to viewModel
            List<WebsiteSelectionListViewModel> websiteViewModels =
                Mapper.Map<List<Website>, List<WebsiteSelectionListViewModel>>(websites);

            return Json(new { websites = websiteViewModels }, JsonRequestBehavior.AllowGet);
        }

        // POST: Get All Available Categories of chosen Website
        [HttpPost]
        [UnitOfWorkFilter]
        [HttpAjaxRequestOnlyFilter]
        [ActionAuthorization(Privilege = "Website,AvailableCategories")]
        public JsonResult GetAvailableCategories(string host)
        {
            if(host == string.Empty)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            // Create specification filter
            WebsiteByHostSpecification websiteByHostSpecification = new WebsiteByHostSpecification(host);

            // Get all websites
            var website = _websiteServices.FindBy(websiteByHostSpecification);

            if(website == null)
                return Json(new { success = false, message = "Error occurred! Please contact our administrators." });

            List<CategorySelectionListViewModel> categoryViewModels = new List<CategorySelectionListViewModel>();
            bool connectivity;
            string message = string.Empty;

            try
            {
                WordPressWrapper core = new WordPressWrapper(website.Host + "/xmlrpc.php", website.Username, website.Password);
                var categories = core.GetCategories().ToList();
                connectivity = true;

                // Convert all categories to viewModels
                categoryViewModels =
                    Mapper.Map<List<Category>, List<CategorySelectionListViewModel>>(categories);
            }
            catch (Exception ex)
            {
                connectivity = false;
                message = ex.Message;
            }

            return Json(new { success = connectivity, message, categories = categoryViewModels });
        }

        #endregion

        #region private methods

        private bool IsPostApprovable(EmployeePrincipal employeePrincipal, string postStatus)
        {
            return employeePrincipal.IsInPrivilege("Post,Approve") &&
                   GenericHelper.ParseEnum<PostStatus>(postStatus) == PostStatus.Submitted;
        }

        private bool IsPostDeliverable(EmployeePrincipal employeePrincipal, string postStatus)
        {
            return employeePrincipal.IsInPrivilege("Post,Delivery") &&
                   GenericHelper.ParseEnum<PostStatus>(postStatus) == PostStatus.Approved;
        }

        private bool IsPostSchedulable(EmployeePrincipal employeePrincipal, string postStatus)
        {
            return employeePrincipal.IsInPrivilege("Post,Schedule") &&
                   GenericHelper.ParseEnum<PostStatus>(postStatus) == PostStatus.Approved;
        }

        private bool IsPostChangeSchedulable(EmployeePrincipal employeePrincipal, string postStatus)
        {
            return employeePrincipal.IsInPrivilege("Post,ChangeSchedule") &&
                   GenericHelper.ParseEnum<PostStatus>(postStatus) == PostStatus.Scheduled;
        }

        private bool IsPostExtraPaymentViewable(EmployeePrincipal employeePrincipal)
        {
            return employeePrincipal.IsInPrivilege("Post,ViewExtraPayments");
        }

        private bool IsPostEditable(EmployeePrincipal employeePrincipal, string postStatus)
        {
            return employeePrincipal.IsInPrivilege("Post,UpdateContent") &&
                   GenericHelper.ParseEnum<PostStatus>(postStatus) == PostStatus.Draft;
        }

        private bool IsPostSubmitable(EmployeePrincipal employeePrincipal, string postStatus)
        {
            return employeePrincipal.IsInPrivilege("Post,Submit") &&
                   GenericHelper.ParseEnum<PostStatus>(postStatus) == PostStatus.Draft;
        }

        private bool IsPostRemovable(EmployeePrincipal employeePrincipal, string postStatus)
        {
            return employeePrincipal.IsInPrivilege("Post,Remove") &&
                   GenericHelper.ParseEnum<PostStatus>(postStatus) == PostStatus.Draft;
        }

        private string GeneratePostCode(int desiredLength)
        {
            string dictionaryString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder resultStringBuilder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < desiredLength; i++)
            {
                resultStringBuilder.Append(dictionaryString[random.Next(dictionaryString.Length)]);
            }
            return resultStringBuilder.ToString();
        }

        private JoeBlogs.Post CreateWordpressPost(Domain.Entities.Post post, Website website, Category category)
        {
            JoeBlogs.Post newWordpressPost = new JoeBlogs.Post
            {
                Title = post.Title,
                Body = post.Content,
                DateCreated = TimeZoneInfo.ConvertTime(DateTime.Now.AddMinutes(-1),
                    GenericHelper.GetTimeZoneInfo(website.TimeZone)),
                Tags = new[] {post.Tags},
                Categories = new[] {category.Name}
            };

            ISEOPlugin chosenPlugin = SeoPluginFactory.CreatePlugin(website.SeoPlugin);
            chosenPlugin.SetupSEOFactors(post.MetaTitle, post.MetaDescription, post.Keywords, string.Empty,
                post.Keywords);
            newWordpressPost.CustomFields = chosenPlugin.createSEOFactors();

            return newWordpressPost;
        }

        private AdminPostListViewModel ReturnAdminPostListViewModelToTable(Domain.Entities.Post post)
        {
            AdminPostListViewModel postViewModel =
                        Mapper.Map<Domain.Entities.Post, AdminPostListViewModel>(post);

            postViewModel.Viewable = true;
            postViewModel.Approvable = IsPostApprovable(User, postViewModel.Status);
            postViewModel.Deliverable = IsPostDeliverable(User, postViewModel.Status);
            postViewModel.Schedulable = IsPostSchedulable(User, postViewModel.Status);
            postViewModel.ChangeSchedulable = IsPostChangeSchedulable(User, postViewModel.Status);
            postViewModel.ViewExtraPayment = IsPostExtraPaymentViewable(User);

            return postViewModel;
        }

        #endregion
    }
}