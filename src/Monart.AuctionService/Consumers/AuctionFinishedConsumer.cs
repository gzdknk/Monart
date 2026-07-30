using Contracts.Auction;
using MassTransit;
using Monart.AuctionService.Data;
using Monart.AuctionService.Entities;

namespace Monart.AuctionService.Consumers
{
    public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
    {
        private readonly AuctionDbContext _dbcontext;

        public AuctionFinishedConsumer(AuctionDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task Consume(ConsumeContext<AuctionFinished> context)
        {
            Console.WriteLine("---> Consuming auction finished");

            var auction = await _dbcontext.Auctions.FindAsync(context.Message.AuctionId);

            if (context.Message.ItemSold)
            {
                auction.Winner = context.Message.Winner;
                auction.SoldAmount = context.Message.Amount;
            }

            auction.Status = auction.SoldAmount > auction.ReservePrice
                ? Status.Finished : Status.ReserveNotMet;

            await _dbcontext.SaveChangesAsync();
        }
    }
}
