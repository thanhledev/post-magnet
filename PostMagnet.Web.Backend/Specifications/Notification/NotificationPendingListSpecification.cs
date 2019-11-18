using System;
using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Web.Backend.Specifications
{
    public class NotificationPendingListSpecification : SpecificationBase<Notification>
    {
        public override System.Linq.Expressions.Expression<Func<Notification, bool>> SpecExpression
        {
            get { return n => n.IsRead == false; }
        }
    }
}