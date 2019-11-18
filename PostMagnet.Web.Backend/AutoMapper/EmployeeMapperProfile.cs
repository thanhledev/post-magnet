using System.Collections.Generic;
using System.Linq;

using PostMagnet.Domain.Entities;
using PostMagnet.Web.Backend.Models;
using PostMagnet.Web.Backend.Security;

using AutoMapper;

namespace PostMagnet.Web.Backend
{
    public class EmployeeMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Employee, EmployeeProfileViewModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
            Mapper.CreateMap<EmployeePrincipal, EmployeeProfileViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.EmployeeRole));
            Mapper.CreateMap<Employee, EmployeePrincipalSerializeModel>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.EmployeeRole, opt => opt.MapFrom(src => src.Role.Name))
                .ForSourceMember(src => src.Password, opt => opt.Ignore())
                .ForSourceMember(src => src.Posts, opt => opt.Ignore())
                .ForSourceMember(src => src.Rate, opt => opt.Ignore())
                .ForSourceMember(src => src.Invoices, opt => opt.Ignore())
                .ForSourceMember(src => src.IsActive, opt => opt.Ignore());
            Mapper.CreateMap<EmployeePrincipalSerializeModel, EmployeePrincipal>()
               .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
               .ForMember(dest => dest.EmployeeRole, opt => opt.MapFrom(src => src.EmployeeRole))
               .ConstructUsing(x => new EmployeePrincipal(x.Username));
            Mapper.CreateMap<Employee, EmployeeListViewModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.ViewEmployeeProfile, opt => opt.Ignore())
                .ForMember(dest => dest.UpdateAccessibility, opt => opt.Ignore())
                .ForSourceMember(src => src.Posts, opt => opt.Ignore())
                .ForSourceMember(src => src.Invoices, opt => opt.Ignore())
                .ForSourceMember(src => src.OwnEmployees, opt => opt.Ignore())
                .ForSourceMember(src => src.Creator, opt => opt.Ignore())
                .ForSourceMember(src => src.Rate, opt => opt.Ignore())
                .ForSourceMember(src => src.Password, opt => opt.Ignore());
            Mapper.CreateMap<EmployeeChangeProfileViewModel, Employee>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            Mapper.CreateMap<Employee, EmployeeProfileViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.Privileges,
                    opt => opt.MapFrom(src => src.Role.Permissions.Select(p => p.Controller + "," + p.Action).ToArray()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForSourceMember(src => src.Password, opt => opt.Ignore());
            Mapper.CreateMap<EmployeeCreationViewModel, Employee>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => emptyPosts()))
                .ForMember(dest => dest.Invoices, opt => opt.MapFrom(src => emptyInvoices()))
                .ForSourceMember(src => src.Password, opt => opt.Ignore())
                .ForSourceMember(src => src.ConfirmPassword, opt => opt.Ignore())
                .ForSourceMember(src => src.RoleId, opt => opt.Ignore());
        }

        private List<Post> emptyPosts()
        {
            return new List<Post>();
        }

        private List<Invoice> emptyInvoices()
        {
            return new List<Invoice>();
        }
    }
}