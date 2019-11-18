using PostMagnet.Domain.Entities;
using PostMagnet.Domain.Repositories;
using PostMagnet.Domain.SpecificationFramework;
using PostMagnet.Services.Internal;

namespace PostMagnet.Web.Backend.Services
{
    public class RoleServices : IRoleServices
    {
        // Repositories will be injected
        IRoleRepository _roleRepository;

        public RoleServices(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public void Create(Role role)
        {
            _roleRepository.Create(role);
        }

        public void Update(Role role)
        {
            _roleRepository.Update(role);
        }

        public Role GetById(int id)
        {
            return _roleRepository.GetById(id);
        }

        public Role FindBy(System.Linq.Expressions.Expression<System.Func<Role, bool>> predicate)
        {
            return _roleRepository.FindBy(predicate);
        }

        public Role FindBy(ISpecification<Role> spec)
        {
            return _roleRepository.FindBy(spec);
        }

        public System.Linq.IQueryable<Role> List()
        {
            return _roleRepository.List();
        }

        public System.Linq.IQueryable<Role> FindList(System.Linq.Expressions.Expression<System.Func<Role, bool>> predicate)
        {
            return _roleRepository.FindList(predicate);
        }

        public System.Linq.IQueryable<Role> FindList(ISpecification<Role> spec)
        {
            return _roleRepository.FindList(spec);
        }
    }
}