using System;
using PostMagnet.Domain.SpecificationFramework;
using System.Linq.Expressions;

namespace PostMagnet.Web.Backend.Specifications
{
    public class PostListByContributorSupervisorSpecification : SpecificationBase<PostMagnet.Domain.Entities.Post>
    {
        private int _creatorId;

        public PostListByContributorSupervisorSpecification(int creatorId)
        {
            _creatorId = creatorId;
        }

        public override Expression<Func<PostMagnet.Domain.Entities.Post, bool>> SpecExpression
        {
            get { return p => p.Contributor.Creator.Id == _creatorId; }
        }
    }
}