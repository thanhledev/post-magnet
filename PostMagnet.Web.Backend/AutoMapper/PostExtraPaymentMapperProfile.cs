using System;

using PostMagnet.Domain.Entities;
using PostMagnet.Web.Backend.Models;

using AutoMapper;

namespace PostMagnet.Web.Backend.AutoMapper
{
    public class PostExtraPaymentMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<PostExtraPayment, PostExtraPaymentListViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount))
                .ForMember(d => d.Note, o => o.MapFrom(s => s.Note))
                .ForMember(d => d.PostId, o => o.MapFrom(s => s.Post.Id));
            Mapper.CreateMap<PostExtraPaymentUpdateViewModel, PostExtraPayment>()
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount))
                .ForMember(d => d.Note, o => o.MapFrom(s => s.Note))
                .ForSourceMember(s => s.Id, o => o.Ignore())
                .ForSourceMember(s => s.PostId, o => o.Ignore());
            Mapper.CreateMap<PostExtraPaymentCreateViewModel, PostExtraPayment>()
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount))
                .ForMember(d => d.Note, o => o.MapFrom(s => s.Note))
                .ForSourceMember(s => s.PostId, o => o.Ignore());
            Mapper.CreateMap<PostExtraPayment, PostExtraPaymentInvoiceViewModel>()
                .ForMember(d => d.Amount, o => o.MapFrom(s => string.Format("{0:n0}", s.Amount)))
                .ForMember(d => d.Note, o => o.MapFrom(s => s.Note));
        }
    }
}