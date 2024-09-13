using AuctionService.DTOs;
using AutoMapper;
using CAS.Business.AuctionService.Commands.Request.Auction;
using CAS.Domain.AuctionService.Entities;

namespace AuctionService.RequestHelper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item);
            CreateMap<Item, AuctionDto>();
            CreateMap<CreateAuctionCommand, Auction>()
                .ForMember(d => d.Item, o => o.MapFrom(s => s));
            CreateMap<CreateAuctionCommand, Item>();
        }
    }
}
