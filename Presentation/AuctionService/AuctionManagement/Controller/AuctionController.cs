using AuctionService.Data;
using AuctionService.DTOs;
using AutoMapper;
using CAS.Business.AuctionManagement.Queries.Request.Auction;
using CAS.Business.AuctionService.Commands.Request.Auction;
using CAS.Core.Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagement.Controller
{
    [ApiController]
    [Route("api/Auctions")]
    public class AuctionController : CASController
    {
        [HttpPost, Route("Auctions")]
        public async Task<IActionResult> CreateAuction([FromBody] CreateAuctionCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet, Route("Auctions")]
        public async Task<IActionResult> GetAuction([FromQuery] GetAuctionQuery request)
        {
            return Ok(await _mediator.Send(request));
        }





        //[HttpGet] //var
        //public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
        //{
        //    var auctions = await _context.Auctions
        //        .Include(x => x.Item)
        //        .OrderBy(x => x.Item.Make)
        //        .ToListAsync();

        //    return _mapper.Map<List<AuctionDto>>(auctions);
        //}
    }
}
