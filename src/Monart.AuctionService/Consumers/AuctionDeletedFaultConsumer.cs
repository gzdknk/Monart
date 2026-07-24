using Contracts.Auction;
using MassTransit;

namespace Monart.AuctionService.Consumers
{
    public class AuctionDeletedFaultConsumer : IConsumer<Fault<AuctionDeleted>>
    {
        public async Task Consume(ConsumeContext<Fault<AuctionDeleted>> context)
        {
            Console.WriteLine("------> Consuming faulty creation");

            var exception = context.Message.Exceptions.First();

            if (exception.ExceptionType == "System.ArgumentException")
            {
                await context.Publish(context.Message.Message);
            }
            else
            {
                Console.WriteLine("Not an argument exception - update error dashboard somewhere");
            }
        }
    }
}
