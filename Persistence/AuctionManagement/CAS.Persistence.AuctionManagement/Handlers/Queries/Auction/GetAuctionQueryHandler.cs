using AuctionService.Data;
using AutoMapper;
using CAS.Business.AuctionManagement.Queries.Request.Auction;
using CAS.Business.AuctionManagement.Queries.Response.Auction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CAS.Persistence.AuctionManagement.Handlers.Queries.Auction
{
    public class GetAuctionQueryHandler : IRequestHandler<GetAuctionQuery, List<GetAuctionQueryResponse>>
    {
        private readonly AuctionDbContext _repository;
        private readonly IMapper _mapper;

        public GetAuctionQueryHandler(AuctionDbContext repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAuctionQueryResponse>> Handle(GetAuctionQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var AuctionList = new List<GetAuctionQueryResponse>();
                var data = _repository.Auctions.Include(x => x.Item).OrderBy(x => x.Item.Make).AsQueryable();

                if (request.Id.HasValue)
                    data = data.Where(x => x.Id == request.Id.Value);
                if (!string.IsNullOrEmpty(request.Seller))
                    data = data.Where(x => x.Seller == request.Seller);

                var auctionItems = await data.ToListAsync(cancellationToken);

                foreach (var item in auctionItems)
                {
                    var mappedItem = _mapper.Map<GetAuctionQueryResponse>(item);
                    AuctionList.Add(mappedItem);
                }

                return AuctionList;
            }
            catch (Exception ex)
            {

                throw; // İstisnayı yeniden fırlat
            }
        }
    }
}
