using System;

using PostMagnet.Domain.Entities;
using PostMagnet.Web.Backend.Models;

using AutoMapper;

namespace PostMagnet.Web.Backend
{
    public class PostMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Post, AdminPostListViewModel>()
                .ForMember(d => d.Code, o => o.MapFrom(s => s.Code))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Author, o => o.MapFrom(s => s.Contributor.Username))
                .ForMember(d => d.Created, o => o.MapFrom(s => s.Created.ToString("MMMM dd yyyy HH:mm:ss")))
                .ForMember(d => d.Submitted,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Submitted != DateTime.MinValue
                                    ? s.Submitted.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.Approved,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Approved != DateTime.MinValue
                                    ? s.Approved.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.UniquePercentage, o => o.MapFrom(s => s.UniquePercentage))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Scheduled,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Scheduled != DateTime.MinValue
                                    ? s.Scheduled.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.Posted,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Posted != DateTime.MinValue
                                    ? s.Posted.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.PostUrl, o => o.MapFrom(s => s.Url))
                .ForMember(d => d.ExtraPaymentCount, o => o.MapFrom(s => s.ExtraPayments.Count));

            Mapper.CreateMap<Post, ContributorPostListViewModel>()
                .ForMember(d => d.Code, o => o.MapFrom(s => s.Code))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Created, o => o.MapFrom(s => s.Created.ToString("MMMM dd yyyy HH:mm:ss")))
                .ForMember(d => d.Submitted,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Submitted != DateTime.MinValue
                                    ? s.Submitted.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.Approved,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Approved != DateTime.MinValue
                                    ? s.Approved.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.UniquePercentage, o => o.MapFrom(s => s.UniquePercentage))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Scheduled,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Scheduled != DateTime.MinValue
                                    ? s.Scheduled.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.Posted,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Posted != DateTime.MinValue
                                    ? s.Posted.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.PostUrl, o => o.MapFrom(s => s.Url))
                .ForMember(d => d.ExtraPaymentCount, o => o.MapFrom(s => s.ExtraPayments.Count));

            Mapper.CreateMap<Post, AdminPostDetailViewModel>()
                .ForMember(d => d.Code, o => o.MapFrom(s => s.Code))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Author, o => o.MapFrom(s => s.Contributor.Username))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.Content))
                .ForMember(d => d.Created, o => o.MapFrom(s => s.Created.ToString("MMMM dd yyyy HH:mm:ss")))
                .ForMember(d => d.Submitted,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Submitted != DateTime.MinValue
                                    ? s.Submitted.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.Approved,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Approved != DateTime.MinValue
                                    ? s.Approved.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.Keywords, o => o.MapFrom(s => s.Keywords))
                .ForMember(d => d.MetaTitle, o => o.MapFrom(s => s.MetaTitle))
                .ForMember(d => d.MetaDescription, o => o.MapFrom(s => s.MetaDescription))
                .ForMember(d => d.UniquePercentage, o => o.MapFrom(s => s.UniquePercentage))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Scheduled,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Scheduled != DateTime.MinValue
                                    ? s.Scheduled.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.Posted,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Posted != DateTime.MinValue
                                    ? s.Posted.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.PostUrl, o => o.MapFrom(s => s.Url))
                .ForMember(d => d.ExtraPaymentCount, o => o.MapFrom(s => s.ExtraPayments.Count));

            Mapper.CreateMap<Post, ContributorPostDetailViewModel>()
                .ForMember(d => d.Code, o => o.MapFrom(s => s.Code))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.Content))
                .ForMember(d => d.Created, o => o.MapFrom(s => s.Created.ToString("MMMM dd yyyy HH:mm:ss")))
                .ForMember(d => d.Submitted,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Submitted != DateTime.MinValue
                                    ? s.Submitted.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.Approved,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Approved != DateTime.MinValue
                                    ? s.Approved.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.Keywords, o => o.MapFrom(s => s.Keywords))
                .ForMember(d => d.MetaTitle, o => o.MapFrom(s => s.MetaTitle))
                .ForMember(d => d.MetaDescription, o => o.MapFrom(s => s.MetaDescription))
                .ForMember(d => d.UniquePercentage, o => o.MapFrom(s => s.UniquePercentage))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Scheduled,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Scheduled != DateTime.MinValue
                                    ? s.Scheduled.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.Posted,
                    o =>
                        o.MapFrom(
                            s =>
                                s.Posted != DateTime.MinValue
                                    ? s.Posted.ToString("MMMM dd yyyy HH:mm:ss")
                                    : string.Empty))
                .ForMember(d => d.PostUrl, o => o.MapFrom(s => s.Url))
                .ForMember(d => d.ExtraPaymentCount, o => o.MapFrom(s => s.ExtraPayments.Count));

            Mapper.CreateMap<ContributorCreatePostViewModel, Post>()
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.Content))
                .ForMember(d => d.Keywords, o => o.MapFrom(s => s.Keywords))
                .ForMember(d => d.MetaTitle, o => o.MapFrom(s => s.MetaTitle))
                .ForMember(d => d.MetaDescription, o => o.MapFrom(s => s.MetaDescription))
                .ForSourceMember(s => s.Status, o => o.Ignore());
            Mapper.CreateMap<ContributorUpdatePostViewModel, Post>()
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.Content))
                .ForMember(d => d.Keywords, o => o.MapFrom(s => s.Keywords))
                .ForMember(d => d.MetaTitle, o => o.MapFrom(s => s.MetaTitle))
                .ForMember(d => d.MetaDescription, o => o.MapFrom(s => s.MetaDescription))
                .ForSourceMember(s => s.Code, o => o.Ignore())
                .ForSourceMember(s => s.Status, o => o.Ignore());
            Mapper.CreateMap<Post, QuickPostScheduleInformationViewModel>()
                .ForMember(d => d.Host, o => o.MapFrom(s => s.ScheduledWebsite))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.ScheduledCategory))
                .ForMember(d => d.Scheduled, o => o.MapFrom(s => s.Scheduled.ToString("MMMM dd yyyy HH:mm:ss")));
        }
    }
}