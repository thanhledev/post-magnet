using System;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Web.Backend.Specifications
{
    public class MessageListByUsernameSpecification : SpecificationBase<Domain.Entities.Message>
    {
        private string _username;

        public MessageListByUsernameSpecification(string username)
        {
            _username = username;
        }

        public override System.Linq.Expressions.Expression<Func<Domain.Entities.Message, bool>> SpecExpression
        {
            get { return m => m.Recipient.Username == _username; }
        }
    }
}