using System;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Web.Backend.Specifications
{
    public class MessageUnreadListSpecification : SpecificationBase<Domain.Entities.Message>
    {
        public override System.Linq.Expressions.Expression<Func<Domain.Entities.Message, bool>> SpecExpression
        {
            get { return m => m.IsRead == false; }
        }
    }
}