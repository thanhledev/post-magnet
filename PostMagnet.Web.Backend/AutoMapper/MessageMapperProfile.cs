using System.Collections.Generic;
using System.Linq;

using PostMagnet.Domain.Entities;
using PostMagnet.Web.Backend.Models;
using PostMagnet.Web.Backend.Security;

using AutoMapper;

namespace PostMagnet.Web.Backend
{
    public class MessageMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Message, MessageListViewModel>()
                .ForMember(d => d.Code, o => o.MapFrom(s => s.Code))
                .ForMember(d => d.Sent, o => o.MapFrom(s => s.Sent.ToString("MMMM dd yyyy HH:mm:ss")))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.Content))
                .ForMember(d => d.IsRead, o => o.MapFrom(s => s.IsRead))
                .ForMember(d => d.Sender, o => o.MapFrom(s => s.Author.Username));
        }
    }
}