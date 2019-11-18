using System;

namespace PostMagnet.Domain.Entities
{
    public class Notification
    {
        public virtual int Id { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual NotificationType Type { get; set; }
        public virtual string Content { get; set; }
        public virtual bool IsRead { get; set; }
        public virtual Employee Receiver { get; set; }
    }
}
