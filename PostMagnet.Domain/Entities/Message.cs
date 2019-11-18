using System;

namespace PostMagnet.Domain.Entities
{
    public class Message
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual DateTime Sent { get; set; }
        public virtual string Content { get; set; }
        public virtual bool IsRead { get; set; }
        public virtual Employee Author { get; set; }
        public virtual Employee Recipient { get; set; }
    }
}
