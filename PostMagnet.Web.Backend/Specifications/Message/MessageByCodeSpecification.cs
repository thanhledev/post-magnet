using System;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Web.Backend.Specifications
{
    public class MessageByCodeSpecification : SpecificationBase<Domain.Entities.Message>
    {
        private string _code;

        public MessageByCodeSpecification(string code)
        {
            _code = code;
        }

        public override System.Linq.Expressions.Expression<Func<Domain.Entities.Message, bool>> SpecExpression
        {
            get { return m => m.Code == _code; }
        }
    }
}