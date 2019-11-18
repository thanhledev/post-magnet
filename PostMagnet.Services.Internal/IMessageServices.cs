using System;
using System.Linq;
using System.Linq.Expressions;

using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Services.Internal
{
    public interface IMessageServices
    {
        void Create(Message message);
        void Update(Message message);

        Message GetById(int id);
        Message FindBy(Expression<Func<Message, bool>> predicate);
        Message FindBy(ISpecification<Message> spec);

        IQueryable<Message> List();
        IQueryable<Message> FindList(Expression<Func<Message, bool>> predicate);
        IQueryable<Message> FindList(ISpecification<Message> spec);
    }
}
