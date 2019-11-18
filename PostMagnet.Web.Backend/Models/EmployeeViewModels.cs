using System.ComponentModel.DataAnnotations;

namespace PostMagnet.Web.Backend.Models
{
    public class EmployeeLoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class EmployeeListViewFilterModel
    {
        public string FilterActiveSelectOption { get; set; }
    }

    public class EmployeeListViewModel
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public bool ViewEmployeeProfile { get; set; }
        public bool UpdateAccessibility { get; set; }
    }

    public class EmployeeProfileViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public string[] Privileges { get; set; }
    }

    public class EmployeeChangeProfileViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class EmployeeChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }

    public class EmployeeQuickUpdateAccessibilityViewModel
    {
        [Required]
        public string Username { get; set; }
    }

    public class EmployeeUpdateAccessibilityViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }

    public class EmployeeResetPasswordViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }

    public class EmployeeCreationViewModel
    {
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
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int RoleId { get; set; }

        public int Rate { get; set; }
    }

    public class WriterBloodhoundListViewModel
    {
        public string username { get; set; }
    }

    public class EmployeeRateUpdateViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int Rate { get; set; }
    }
}