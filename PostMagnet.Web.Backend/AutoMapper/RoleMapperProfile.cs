using PostMagnet.Domain.Entities;
using PostMagnet.Web.Backend.Models;

using AutoMapper;

namespace PostMagnet.Web.Backend
{
    public class RoleMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Role, RoleListViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForSourceMember(src => src.Permissions, opt => opt.Ignore())
                .ForSourceMember(src => src.Employees, opt => opt.Ignore());
        }
    }
}