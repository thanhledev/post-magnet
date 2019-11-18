using System;
using System.Linq.Expressions;

namespace PostMagnet.Domain.SpecificationFramework
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> SpecExpression { get; }
        bool IsSatisfiedBy(T obj);
    }
}
