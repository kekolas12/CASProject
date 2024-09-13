using AutoMapper;
using CAS.Business.AuctionManagement.Queries.Response.Auction;
using CAS.Business.AuctionService.Commands.Request.Auction;
using CAS.Business.AuctionService.Commands.Response.Auction;
using CAS.Core.Infrastructure.Mapper;
using CAS.Domain.AuctionService.Entities;

namespace CAS.Persistence.AuctionManagement.Handlers.Extensions.Mapper.Profiles.Auction
{
    public class AuctionProfile : Profile, IAutoMapperProfile
    {
        public int Order => 0;
        public AuctionProfile()
        {
            CreateMap<Domain.AuctionService.Entities.Auction, AuctionCommandResponse>()
                .IncludeMembers(x => x.Item);
            CreateMap<Domain.AuctionService.Entities.Auction, GetAuctionQueryResponse>();
            CreateMap<GetAuctionQueryResponse, Domain.AuctionService.Entities.Auction>();
            CreateMap<Item, AuctionCommandResponse>();
            CreateMap<CreateAuctionCommand, Domain.AuctionService.Entities.Auction>()
                .ForMember(d => d.Item, o => o.MapFrom(s => s));
            CreateMap<CreateAuctionCommand, Item>();
        }
    }
}
