using CAS.Business.AuctionManagement.Queries.Response.Auction;
using CAS.Shared.Models;
using MediatR;

namespace CAS.Business.AuctionManagement.Queries.Request.Auction
{
    public class GetAuctionQuery : BaseEntityModel, IRequest<List<GetAuctionQueryResponse>>
    {
        public string Seller { get; set; }
    }
}
