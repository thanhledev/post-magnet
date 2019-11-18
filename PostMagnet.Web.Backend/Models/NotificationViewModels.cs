namespace PostMagnet.Web.Backend.Models
{
    public class NotificationListViewModel
    {
        public int Id { get; set; }
        public string Created { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
    }

    public class NotificationMenuListViewModel
    {
        public string Type { get; set; }
        public string Created { get; set; }
        public string Content { get; set; }
    }
}