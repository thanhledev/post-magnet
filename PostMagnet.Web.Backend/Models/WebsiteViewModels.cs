using System;
using System.ComponentModel.DataAnnotations;

namespace PostMagnet.Web.Backend.Models
{
    public class WebsiteListViewModel
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Tested { get; set; }
        public string Note { get; set; }
        public bool Testable { get; set; }
        public bool Editable { get; set; }
    }

    public class WebsiteDetailViewModel
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TimeZone { get; set; }
        public string SeoPluginType { get; set; }
        public string Tested { get; set; }
        public string Note { get; set; }
    }

    public class WebsiteUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Host { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }        
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string TimeZone { get; set; }
        [Required]
        public string SeoPluginType { get; set; }

        public bool RequireTesting { get; set; }
    }

    public class WebsiteCheckConnectionViewModel
    {
        [Required]
        public int Id { get; set; }
    }

    public class WebsiteCreationViewModel
    {
        [Required]
        public string Host { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string TimeZone { get; set; }
        [Required]
        public string SeoPluginType { get; set; }

        public bool RequireTesting { get; set; }
    }

    public class WebsiteSelectionListViewModel
    {
        public string Host { get; set; }
        public string Tested { get; set; }
    }

    public class CategorySelectionListViewModel
    {
        public string Name { get; set; }
    }

    public class TimeZoneListViewModel
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
    }
}