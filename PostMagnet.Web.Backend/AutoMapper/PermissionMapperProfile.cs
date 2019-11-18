using PostMagnet.Domain.Entities;
using PostMagnet.Web.Backend.Models;

using AutoMapper;

namespace PostMagnet.Web.Backend
{
    public class PermissionMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Permission, PermissionListViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            Mapper.CreateMap<Permission, PermissionMenuListViewModel>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Controller, o => o.MapFrom(s => s.Controller))
                .ForMember(d => d.Action, o => o.MapFrom(s => s.Action))
                .ForSourceMember(s => s.Id, o => o.Ignore())
                .ForSourceMember(s => s.Roles, o => o.Ignore());
        }
    }
}