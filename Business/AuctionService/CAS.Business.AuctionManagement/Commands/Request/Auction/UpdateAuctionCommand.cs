using CAS.Business.AuctionService.Commands.Response.Auction;
using MediatR;

namespace CAS.Business.AuctionService.Commands.Request.Auction
{
    public class UpdateAuctionCommand : IRequest<AuctionCommandResponse>
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public string Color { get; set; }
        public int? Mileage { get; set; }
    }
}
