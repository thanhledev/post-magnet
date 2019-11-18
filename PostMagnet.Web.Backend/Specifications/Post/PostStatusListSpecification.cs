using System;
using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;
using System.Linq.Expressions;

namespace PostMagnet.Web.Backend.Specifications
{
    public class PostStatusListSpecification : SpecificationBase<PostMagnet.Domain.Entities.Post>
    {
        private PostStatus? _postStatus;

        public PostStatusListSpecification(PostStatus? postStatus)
        {
            _postStatus = postStatus;
        }

        public override Expression<Func<PostMagnet.Domain.Entities.Post, bool>> SpecExpression
        {
            get
            {
                if (_postStatus.HasValue)
                    return p => p.Status == _postStatus.Value;
                return p => true;
            }
        }
    }
}