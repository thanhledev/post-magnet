using System;
using PostMagnet.Domain.SpecificationFramework;
using System.Linq.Expressions;

namespace PostMagnet.Web.Backend.Specifications
{
    public class PostListByContributorSpecification : SpecificationBase<PostMagnet.Domain.Entities.Post>
    {
        private int _contributorId;

        public PostListByContributorSpecification(int contributorId)
        {
            _contributorId = contributorId;
        }

        public override Expression<Func<PostMagnet.Domain.Entities.Post, bool>> SpecExpression
        {
            get { return p => p.Contributor.Id == _contributorId; }
        }
    }
}