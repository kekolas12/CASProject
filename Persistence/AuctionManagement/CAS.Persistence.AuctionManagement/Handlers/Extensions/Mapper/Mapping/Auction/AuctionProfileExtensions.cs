using CAS.Business.AuctionService.Commands.Request.Auction;
using CAS.Business.AuctionService.Commands.Response.Auction;
using CAS.Core.Infrastructure.Mapper;

namespace CAS.Persistence.AuctionManagement.Handlers.Extensions.Mapper.Mapping.Auction
{
    public static class AuctionProfileExtensions
    {
        public static AuctionCommandResponse ToModel(this Domain.AuctionService.Entities.Auction model)
        {
            return model.MapTo<Domain.AuctionService.Entities.Auction, AuctionCommandResponse>();
        }
        public static Domain.AuctionService.Entities.Auction ToEntity(this CreateAuctionCommand model)
        {
            return model.MapTo<CreateAuctionCommand, Domain.AuctionService.Entities.Auction>();
        }
        public static Domain.AuctionService.Entities.Auction ToModel(this CreateAuctionCommand source, Domain.AuctionService.Entities.Auction destination)
        {
            return source.MapTo(destination);
        }
    }
}
