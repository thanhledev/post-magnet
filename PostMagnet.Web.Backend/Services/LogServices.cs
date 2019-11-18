using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Repositories;
using PostMagnet.Domain.SpecificationFramework;
using PostMagnet.Services.Internal;

namespace PostMagnet.Web.Backend.Services
{
    public class LogServices : ILogServices
    {
        // Repositories will be injected
        ILogRepository _logRepository;

        public LogServices(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public void Create(Log log)
        {
            _logRepository.Create(log);
        }

        public void Update(Log log)
        {
            _logRepository.Update(log);
        }

        public Log GetById(int id)
        {
            return _logRepository.GetById(id);
        }

        public Log FindBy(System.Linq.Expressions.Expression<System.Func<Log, bool>> predicate)
        {
            return _logRepository.FindBy(predicate);
        }

        public Log FindBy(ISpecification<Log> spec)
        {
            return _logRepository.FindBy(spec);
        }

        public System.Linq.IQueryable<Log> List()
        {
            return _logRepository.List();
        }

        public System.Linq.IQueryable<Log> FindList(System.Linq.Expressions.Expression<System.Func<Log, bool>> predicate)
        {
            return _logRepository.FindList(predicate);
        }

        public System.Linq.IQueryable<Log> FindList(ISpecification<Log> spec)
        {
            return _logRepository.FindList(spec);
        }
    }
}