using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Repositories;
using PostMagnet.Domain.SpecificationFramework;
using PostMagnet.Services.Internal;

namespace PostMagnet.Web.Backend.Services
{
    public class WebsiteServices : IWebsiteServices
    {
        // Repositories will be injected
        IWebsiteRepository _websiteRepository;

        public WebsiteServices(IWebsiteRepository websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }

        public void Create(Website website)
        {
            _websiteRepository.Create(website);
        }

        public void Update(Website website)
        {
            _websiteRepository.Update(website);
        }

        public Website GetById(int id)
        {
            return _websiteRepository.GetById(id);
        }

        public Website FindBy(System.Linq.Expressions.Expression<System.Func<Website, bool>> predicate)
        {
            return _websiteRepository.FindBy(predicate);
        }

        public Website FindBy(ISpecification<Website> spec)
        {
            return _websiteRepository.FindBy(spec);
        }

        public System.Linq.IQueryable<Website> List()
        {
            return _websiteRepository.List();
        }

        public System.Linq.IQueryable<Website> FindList(System.Linq.Expressions.Expression<System.Func<Website, bool>> predicate)
        {
            return _websiteRepository.FindList(predicate);
        }

        public System.Linq.IQueryable<Website> FindList(ISpecification<Website> spec)
        {
            return _websiteRepository.FindList(spec);
        }
    }
}