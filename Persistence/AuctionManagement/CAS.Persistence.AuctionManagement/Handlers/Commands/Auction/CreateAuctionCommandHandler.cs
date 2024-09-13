using AuctionService.Data;
using AutoMapper;
using CAS.Business.AuctionService.Commands.Request.Auction;
using CAS.Business.AuctionService.Commands.Response.Auction;
using CAS.Domain.AuctionService.Entities;
using CAS.Persistence.AuctionManagement.Handlers.Extensions.Mapper.Mapping.Auction;
using CAS.Shared.DataSource;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using static MassTransit.ValidationResultExtensions;

namespace CAS.Persistence.Auction.Handlers.Commands.Auction
{
    public class CreateAuctionCommandHandler : IRequestHandler<CreateAuctionCommand, DataSourceResult<AuctionCommandResponse>>
    {
        private readonly AuctionDbContext _repository;
        private readonly IMapper _mapper;

        public CreateAuctionCommandHandler(IMapper mapper, AuctionDbContext repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DataSourceResult<AuctionCommandResponse>> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var mappedModel = request.ToEntity();
                _repository.Auctions.Add(mappedModel);
                var result = mappedModel.ToModel();

                #region
                //var mappedmodel = _mapper.Map<Domain.AuctionService.Entities.Auction>(request);
                //mappedmodel.Seller = "Test";

                //_repository.Auctions.Add(mappedmodel);

                //var IsSucces = await _repository.SaveChangesAsync() > 0;

                //if (!IsSucces)
                //    return null;

                //var result = mappedmodel.ToModel();
                #endregion

                return DataSourceResult<AuctionCommandResponse>.Success(result, 200);
            }
            catch (Exception ex)
            {
                return DataSourceResult<AuctionCommandResponse>.Fail("Başaramadın kanka", 500);
            }
        }
    }
}