using Contracts.Auction;
using MassTransit;
using Monart.SearchService.Entities;
using MongoDB.Entities;

namespace Monart.SearchService.Consumers
{
    public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
    {
        public async Task Consume(ConsumeContext<AuctionFinished> context)
        {
            Console.WriteLine("----> Consuming auction finished");

            var auction = await DB.Default.Find<Item>().OneAsync(context.Message.AuctionId);

            if (context.Message.ItemSold)
            {
                auction.Winner = context.Message.Winner;
                auction.SoldAmount = (decimal)context.Message.Amount;
            }

            auction.Status = "Finished";

            await DB.Default.SaveAsync(auction);
        }
    }
}
