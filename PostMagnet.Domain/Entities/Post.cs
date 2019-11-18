using System;
using System.Collections.Generic;

namespace PostMagnet.Domain.Entities
{
    public class Post
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual string Keywords { get; set; }
        public virtual string MetaTitle { get; set; }
        public virtual string MetaDescription { get; set; }
        public virtual string Tags { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime Submitted { get; set; }
        public virtual DateTime Approved { get; set; }
        public virtual PostStatus Status { get; set; }
        public virtual int UniquePercentage { get; set; }
        public virtual DateTime Scheduled { get; set; }
        public virtual string ScheduledWebsite { get; set; }
        public virtual string ScheduledCategory { get; set; }
        public virtual DateTime Posted { get; set; }
        public virtual string Url { get; set; }
        public virtual string Note { get; set; }
        public virtual Employee Contributor { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual IList<PostExtraPayment> ExtraPayments { get; set; }

        public Post()
        {
            ExtraPayments = new List<PostExtraPayment>();
        }
    }
}
