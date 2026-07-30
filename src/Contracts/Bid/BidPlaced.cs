using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Bid
{
    public class BidPlaced
    {
        public string Id { get; set; }
        public string AuctionId { get; set; }
        public string Bidder { get; set; }
        public DateTime BidTime { get; set; }
        public decimal Amount { get; set; }
        public string BidStatus { get; set; }
    }
}
