namespace PostMagnet.Domain.SpecificationFramework
{
    public abstract class CompositeSpecificationBase<T> : SpecificationBase<T>
    {
        private readonly ISpecification<T> _leftExpr;
        private readonly ISpecification<T> _rightExpr;

        protected CompositeSpecificationBase(ISpecification<T> left, ISpecification<T> right)
        {
            _leftExpr = left;
            _rightExpr = right;
        }

        public ISpecification<T> Left { get { return _leftExpr; } }
        public ISpecification<T> Right { get { return _rightExpr; } }
    }
}
