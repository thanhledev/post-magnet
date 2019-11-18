using System;
using System.Linq.Expressions;

namespace PostMagnet.Domain.SpecificationFramework
{
    public class OrSpecification<T> : CompositeSpecificationBase<T>
    {
        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
            : base(left, right)
        {

        }

        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.OrElse(
                        Expression.Invoke(Left.SpecExpression, objParam),
                        Expression.Invoke(Right.SpecExpression, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }
    }
}
