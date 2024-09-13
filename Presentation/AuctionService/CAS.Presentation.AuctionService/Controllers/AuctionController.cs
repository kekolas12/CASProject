using AuctionService.Data;
using AuctionService.DTOs;
using AutoMapper;
using CAS.Business.AuctionService.Commands.Request.Auction;
using CAS.Core.Infrastructure.Controllers;
using CAS.Domain.AuctionService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AuctionService.Controllers
{
    [ApiController]
    [Route("api/Auctions")]
    public class AuctionController : CASController
    {
        private IMapper _mapper;
        private AuctionDbContext _context;
        public AuctionController(IMapper mapper, AuctionDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }



        [HttpPost("Create")]
        public async Task<IActionResult> CreateAuctionModel([FromBody] CreateAuctionCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


















        [HttpGet] //var
        public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
        {
            var auctions = await _context.Auctions
                .Include(x => x.Item)
                .OrderBy(x => x.Item.Make)
                .ToListAsync();

            return _mapper.Map<List<AuctionDto>>(auctions);
        }

        [HttpGet("{id}")] //var
        public async Task<ActionResult<List<AuctionDto>>> GetAuctionById(Guid Id)
        {
            var auction = await _context.Auctions
                .Include(x => x.Item)
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (auction == null) return NotFound();

            return _mapper.Map<List<AuctionDto>>(auction);
        }


        [HttpPost]
        public async Task<ActionResult<List<AuctionDto>>> CreateAuction(CreateAuctionCommand auctionDto)
        {
            var auction = _mapper.Map<Auction>(auctionDto);
            //TODO: Add current user as seller

            auction.Seller = "Test";

            _context.Auctions.Add(auction);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
                return BadRequest("NOT SAVE TO THE DB");

            return CreatedAtAction(nameof(GetAuctionById),
                new { auction.Id }, _mapper.Map<AuctionDto>(auction));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAuction(UpdateAuctionCommand updateAuctionDto)
        {
            var auction = await _context.Auctions.Include(x => x.Item)
                .FirstOrDefaultAsync(x => x.Id == updateAuctionDto.Id);

            if (auction == null) return NotFound();

            //TODO: CHECK SELLER == USERNAME

            auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
            auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
            auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
            auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;
            auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;


            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest("Bir şeyler hatalı");

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuction(Guid id)
        {
            var auction = await _context.Auctions.FindAsync(id);

            if (auction == null) return NotFound();
            //TODO: check seller == username

            _context.Remove(auction);
            var result = _context.SaveChanges() > 0;

            if (!result) return BadRequest("İşlem yapılamadı");

            return Ok();

        }

    }

}

