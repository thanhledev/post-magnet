namespace PostMagnet.Web.Backend.Models
{
    public class MessageListViewModel
    {
        public string Code { get; set; }
        public string Sent { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public string Sender { get; set; }
    }
}