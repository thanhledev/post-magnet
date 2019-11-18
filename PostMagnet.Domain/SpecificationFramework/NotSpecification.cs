using System;
using System.Linq.Expressions;

namespace PostMagnet.Domain.SpecificationFramework
{
    public class NotSpecification<T> : SpecificationBase<T>
    {
        private readonly ISpecification<T> _inner;

        public NotSpecification(ISpecification<T> inner)
        {
            _inner = inner;
        }

        public ISpecification<T> Inner
        {
            get { return _inner; }
        }

        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.Not(
                        Expression.Invoke(Inner.SpecExpression, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }
    }
}
