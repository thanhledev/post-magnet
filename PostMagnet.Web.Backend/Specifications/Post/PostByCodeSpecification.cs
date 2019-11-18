using System;
using PostMagnet.Domain.SpecificationFramework;
using System.Linq.Expressions;

namespace PostMagnet.Web.Backend.Specifications
{
    public class PostByCodeSpecification : SpecificationBase<Domain.Entities.Post>
    {
        private string _postCode;

        public PostByCodeSpecification(string code)
        {
            _postCode = code;
        }

        public override Expression<Func<PostMagnet.Domain.Entities.Post, bool>> SpecExpression
        {
            get { return p => p.Code == _postCode; }
        }
    }
}