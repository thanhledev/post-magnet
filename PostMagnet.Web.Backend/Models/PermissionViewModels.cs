namespace PostMagnet.Web.Backend.Models
{
    public class PermissionListViewModel
    {
        public string Name { get; set; }
    }

    public class PermissionMenuListViewModel
    {
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}