using System;
using System.Linq;
using System.Linq.Expressions;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Services.Internal
{
    public interface ILogServices
    {
        void Create(Log log);
        void Update(Log log);

        Log GetById(int id);
        Log FindBy(Expression<Func<Log, bool>> predicate);
        Log FindBy(ISpecification<Log> spec);

        IQueryable<Log> List();
        IQueryable<Log> FindList(Expression<Func<Log, bool>> predicate);
        IQueryable<Log> FindList(ISpecification<Log> spec);
    }
}
