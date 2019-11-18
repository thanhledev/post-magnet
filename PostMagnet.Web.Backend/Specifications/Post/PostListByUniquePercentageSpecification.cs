using System;
using PostMagnet.Domain.SpecificationFramework;
using System.Linq.Expressions;

namespace PostMagnet.Web.Backend.Specifications
{
    public class PostListByUniquePercentageSpecification : SpecificationBase<PostMagnet.Domain.Entities.Post>
    {
        private int? _minPercentage;
        private int? _maxPercentage;

        public PostListByUniquePercentageSpecification(int? minPercentage, int? maxPercentage)
        {
            _minPercentage = minPercentage;
            _maxPercentage = maxPercentage;
        }

        public override Expression<Func<PostMagnet.Domain.Entities.Post, bool>> SpecExpression
        {
            get
            {
                if (_minPercentage.HasValue && _maxPercentage.HasValue)
                    return p => p.UniquePercentage >= _minPercentage.Value && p.UniquePercentage <= _maxPercentage.Value;
                return p => true;
            }
        }
    }
}