using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PostMagnet.Web.Backend.Models
{
    public class PostListViewFilterModel
    {
        public string FilterPostStatusOption { get; set; }
        public string FilterPostQualityOption { get; set; }
    }

    public class AdminPostListViewModel
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Created { get; set; }
        public string Submitted { get; set; }
        public string Approved { get; set; }
        public int UniquePercentage { get; set; }
        public string Status { get; set; }
        public string Scheduled { get; set; }
        public string Posted { get; set; }
        public string PostUrl { get; set; }
        public bool Viewable { get; set; }
        public bool Approvable { get; set; }
        public bool Deliverable { get; set; }
        public bool Schedulable { get; set; }
        public bool ChangeSchedulable { get; set; }
        public bool ViewExtraPayment { get; set; }
        public int ExtraPaymentCount { get; set; }
    }

    public class ContributorPostListViewModel
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Created { get; set; }
        public string Submitted { get; set; }
        public string Approved { get; set; }
        public int UniquePercentage { get; set; }
        public string Status { get; set; }
        public string Scheduled { get; set; }
        public string Posted { get; set; }
        public string PostUrl { get; set; }
        public bool Viewable { get; set; }
        public bool Editable { get; set; }
        public bool Removable { get; set; }
        public bool Submitable { get; set; }
        public bool ViewExtraPayment { get; set; }
        public int ExtraPaymentCount { get; set; }
    }

    public class AdminPostDetailViewModel
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string Created { get; set; }
        public string Submitted { get; set; }
        public string Approved { get; set; }
        public string Keywords { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public int UniquePercentage { get; set; }
        public string Status { get; set; }
        public string Scheduled { get; set; }
        public string Posted { get; set; }
        public string PostUrl { get; set; }
        public bool Approvable { get; set; }
        public bool Deliverable { get; set; }
        public bool Schedulable { get; set; }
        public bool ChangeSchedulable { get; set; }
        public bool ViewExtraPayment { get; set; }
        public int ExtraPaymentCount { get; set; }
    }

    public class ContributorPostDetailViewModel
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Created { get; set; }
        public string Submitted { get; set; }
        public string Approved { get; set; }
        public string Keywords { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public int UniquePercentage { get; set; }
        public string Status { get; set; }
        public string Scheduled { get; set; }
        public string Posted { get; set; }
        public string PostUrl { get; set; }
        public bool Editable { get; set; }
        public bool Removable { get; set; }
        public bool Submitable { get; set; }
        public bool ViewExtraPayment { get; set; }
        public int ExtraPaymentCount { get; set; }
    }

    public class ContributorCreatePostViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [AllowHtml]
        public string Content { get; set; }
        [Required]
        public string Keywords { get; set; }
        [Required]
        public string MetaTitle { get; set; }
        [Required]
        public string MetaDescription { get; set; }
        [Required]
        public string Status { get; set; }
    }

    public class ContributorUpdatePostViewModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [AllowHtml]
        public string Content { get; set; }
        [Required]
        public string Keywords { get; set; }
        [Required]
        public string MetaTitle { get; set; }
        [Required]
        public string MetaDescription { get; set; }
        [Required]
        public string Status { get; set; }
    }

    public class QuickApprovePostViewModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Confirmation { get; set; }
        public string Submission { get; set; }
        public string Host { get; set; }
        public string Category { get; set; }
        public string Scheduled { get; set; }
    }

    public class QuickDeliveryPostViewModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Submission { get; set; }
        [Required]
        public string Host { get; set; }
        [Required]
        public string Category { get; set; }
        public string Scheduled { get; set; }
    }

    public class QuickSchedulePostViewModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Submission { get; set; }
        [Required]
        public string Host { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Scheduled { get; set; }
    }

    public class QuickPostScheduleInformationViewModel
    {
        public string Host { get; set; }
        public string Category { get; set; }
        public string Scheduled { get; set; }
    }
}