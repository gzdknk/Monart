using Contracts.Bid;
using MassTransit;
using Monart.SearchService.Entities;
using MongoDB.Entities;

namespace Monart.SearchService.Consumers
{
    public class BidPlacedConsumer : IConsumer<BidPlaced>
    {
        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
            Console.WriteLine("----> Consuming bid placed");

            var auction = await DB.Default.Find<Item>().OneAsync(context.Message.AuctionId);

            if(auction.CurrentHighBid == null
                || context.Message.BidStatus.Contains("Accepted")
                && context.Message.Amount > auction.CurrentHighBid)
            {
                auction.CurrentHighBid = context.Message.Amount;
                
                await DB.Default.SaveAsync(auction);
            }
        }
    }
}
