using System;
using PostMagnet.Domain.Entities;
using PostMagnet.Web.Backend.Models;

using AutoMapper;
using JoeBlogs;
using PostMagnet.Web.Backend.Helpers;

namespace PostMagnet.Web.Backend
{
    public class WebsiteMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Website, WebsiteListViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Host, o => o.MapFrom(s => s.Host))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Username))
                .ForMember(d => d.Tested,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Tested != DateTime.MinValue
                                    ? s.Tested.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.Note, o => o.MapFrom(s => s.Note));
            Mapper.CreateMap<Website, WebsiteDetailViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Host, o => o.MapFrom(s => s.Host))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Username))
                .ForMember(d => d.Password, o => o.MapFrom(s => s.Password))
                .ForMember(d => d.TimeZone, o => o.MapFrom(s => s.TimeZone))
                .ForMember(d => d.SeoPluginType, o => o.MapFrom(s => s.SeoPlugin))
                .ForMember(d => d.Tested,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Tested != DateTime.MinValue
                                    ? s.Tested.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.Note, o => o.MapFrom(s => s.Note));
            Mapper.CreateMap<WebsiteUpdateViewModel, Website>()
                .ForMember(d => d.Host, o => o.MapFrom(s => s.Host))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Username))
                .ForMember(d => d.Password, o => o.MapFrom(s => s.Password))
                .ForMember(d => d.TimeZone, o => o.MapFrom(s => s.TimeZone))
                .ForMember(d => d.SeoPlugin,
                    o => o.MapFrom(s => GenericHelper.ParseEnum<SeoPluginType>(s.SeoPluginType)))
                .ForSourceMember(s => s.Id, o => o.Ignore())
                .ForSourceMember(s => s.ConfirmPassword, o => o.Ignore())
                .ForSourceMember(s => s.RequireTesting, o => o.Ignore());
            Mapper.CreateMap<WebsiteCreationViewModel, Website>()
                .ForMember(d => d.Host, o => o.MapFrom(s => s.Host))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Username))
                .ForMember(d => d.Password, o => o.MapFrom(s => s.Password))
                .ForMember(d => d.TimeZone, o => o.MapFrom(s => s.TimeZone))
                .ForMember(d => d.SeoPlugin,
                    o => o.MapFrom(s => GenericHelper.ParseEnum<SeoPluginType>(s.SeoPluginType)))
                .ForSourceMember(s => s.ConfirmPassword, o => o.Ignore())
                .ForSourceMember(s => s.RequireTesting, o => o.Ignore());
            Mapper.CreateMap<Website, WebsiteSelectionListViewModel>()
                .ForMember(d => d.Host, o => o.MapFrom(s => s.Host))
                .ForMember(d => d.Tested,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Tested != DateTime.MinValue
                                    ? s.Tested.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty));
            Mapper.CreateMap<Category, CategorySelectionListViewModel>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name));

            Mapper.CreateMap<TimeZoneInfo, TimeZoneListViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.DisplayName));
        }
    }
}