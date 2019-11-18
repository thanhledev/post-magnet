using System;
using System.Collections.Generic;
using System.Linq;

using PostMagnet.Domain.Entities;
using PostMagnet.Web.Backend.Models;
using PostMagnet.Web.Backend.Security;

using AutoMapper;

namespace PostMagnet.Web.Backend
{
    public class NotificationMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Notification, NotificationListViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Created, o => o.MapFrom(s => s.Created.ToString("MMMM dd yyyy HH:mm:ss")))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.Content))
                .ForMember(d => d.IsRead, o => o.MapFrom(s => s.IsRead))
                .ForSourceMember(s => s.Type, o => o.Ignore())
                .ForSourceMember(s => s.Receiver, o => o.Ignore());
            Mapper.CreateMap<Notification, NotificationMenuListViewModel>()
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.ToString()))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.Content))
                .ForMember(d => d.Created, o => o.MapFrom(s => CalculateDate(s.Created)));
        }

        private string CalculateDate(DateTime createdDateTime)
        {
            DateTime current = DateTime.Now;

            if ((current - createdDateTime).TotalSeconds < 30)
            {
                return "just now";
            }
            if ((current - createdDateTime).TotalMinutes < 60)
            {
                return (int) (current - createdDateTime).TotalMinutes + " mins";
            }
            if ((current - createdDateTime).TotalHours < 24)
            {
                return (int)(current - createdDateTime).TotalHours + " hrs";
            }
            return (int)(current - createdDateTime).TotalDays + " days";
        }
    }
}