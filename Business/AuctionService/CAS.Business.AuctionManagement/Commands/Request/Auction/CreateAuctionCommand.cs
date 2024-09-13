using CAS.Business.AuctionService.Commands.Response.Auction;
using CAS.Shared.DataSource;
using MediatR;

namespace CAS.Business.AuctionService.Commands.Request.Auction
{
    public class CreateAuctionCommand : IRequest<DataSourceResult<AuctionCommandResponse>>
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public string ImageUrl { get; set; }
        public int ReservePrice { get; set; }
        public DateTime AuctionEnd { get; set; }
    }
}
