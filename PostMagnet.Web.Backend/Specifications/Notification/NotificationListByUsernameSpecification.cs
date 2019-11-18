using System;
using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Web.Backend.Specifications
{
    public class NotificationListByUsernameSpecification : SpecificationBase<Notification>
    {
        private string _username;

        public NotificationListByUsernameSpecification(string username)
        {
            _username = username;
        }

        public override System.Linq.Expressions.Expression<Func<Notification, bool>> SpecExpression
        {
            get { return n => n.Receiver.Username == _username; }
        }
    }
}