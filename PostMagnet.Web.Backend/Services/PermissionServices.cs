using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Repositories;
using PostMagnet.Domain.SpecificationFramework;
using PostMagnet.Services.Internal;

namespace PostMagnet.Web.Backend.Services
{
    public class PermissionServices : IPermissionServices
    {
        // Repositories will be injected
        IPermissonRepository _permissonRepository;

        public PermissionServices(IPermissonRepository permissonRepository)
        {
            _permissonRepository = permissonRepository;
        }

        public void Create(Permission permission)
        {
            _permissonRepository.Create(permission);
        }

        public void Update(Permission permission)
        {
            _permissonRepository.Update(permission);
        }

        public Permission GetById(int id)
        {
            return _permissonRepository.GetById(id);
        }

        public Permission FindBy(System.Linq.Expressions.Expression<System.Func<Permission, bool>> predicate)
        {
            return _permissonRepository.FindBy(predicate);
        }

        public Permission FindBy(ISpecification<Permission> spec)
        {
            return _permissonRepository.FindBy(spec);
        }

        public System.Linq.IQueryable<Permission> List()
        {
            return _permissonRepository.List();
        }

        public System.Linq.IQueryable<Permission> FindList(System.Linq.Expressions.Expression<System.Func<Permission, bool>> predicate)
        {
            return _permissonRepository.FindList(predicate);
        }

        public System.Linq.IQueryable<Permission> FindList(ISpecification<Permission> spec)
        {
            return _permissonRepository.FindList(spec);
        }
    }
}