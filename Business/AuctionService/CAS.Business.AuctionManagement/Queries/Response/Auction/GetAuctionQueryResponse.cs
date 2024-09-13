using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAS.Business.AuctionManagement.Queries.Response.Auction
{
    public class GetAuctionQueryResponse
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
