using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Repositories;
using PostMagnet.Domain.SpecificationFramework;
using PostMagnet.Services.Internal;

namespace PostMagnet.Web.Backend.Services
{
    public class MessageServices : IMessageServices
    {
        // Repositories will be injected
        IMessageRepository _messageRepository;

        public MessageServices(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public void Create(Message message)
        {
            _messageRepository.Create(message);
        }

        public void Update(Message message)
        {
            _messageRepository.Update(message);
        }

        public Message GetById(int id)
        {
            return _messageRepository.GetById(id);
        }

        public Message FindBy(System.Linq.Expressions.Expression<System.Func<Message, bool>> predicate)
        {
            return _messageRepository.FindBy(predicate);
        }

        public Message FindBy(ISpecification<Message> spec)
        {
            return _messageRepository.FindBy(spec);
        }

        public System.Linq.IQueryable<Message> List()
        {
            return _messageRepository.List();
        }

        public System.Linq.IQueryable<Message> FindList(System.Linq.Expressions.Expression<System.Func<Message, bool>> predicate)
        {
            return _messageRepository.FindList(predicate);
        }

        public System.Linq.IQueryable<Message> FindList(ISpecification<Message> spec)
        {
            return _messageRepository.FindList(spec);
        }
    }
}