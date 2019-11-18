using System;
using PostMagnet.Domain.SpecificationFramework;
using PostMagnet.Domain.Entities;
using System.Linq.Expressions;

namespace PostMagnet.Web.Backend.Specifications
{
    public class ExtraPaymentListByPostSpecification : SpecificationBase<PostExtraPayment>
    {
        private string _postCode;

        public ExtraPaymentListByPostSpecification(string code)
        {
            _postCode = code;
        }

        public override Expression<Func<PostExtraPayment, bool>> SpecExpression
        {
            get { return e => e.Post.Code == _postCode; }
        }
    }
}