using PostMagnet.Domain.Entities;
using PostMagnet.Web.Backend.Models;

using AutoMapper;

namespace PostMagnet.Web.Backend
{
    public class LogMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Log, LogListViewModel>()
                .ForMember(d => d.Created, o => o.MapFrom(s => s.Created.ToString("MMMM dd yyyy HH:mm:ss")))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.Content));
        }
    }
}