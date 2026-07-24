using AutoMapper;
using Contracts.Auction;
using MassTransit;
using Monart.SearchService.Entities;
using MongoDB.Entities;

namespace Monart.SearchService.Consumers
{
    public class AuctionDeletedConsumer : IConsumer<AuctionDeleted>
    {
        private readonly IMapper _mapper;

        public AuctionDeletedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<AuctionDeleted> context)
        {
            Console.WriteLine("---> Consuming auction deleted: " + context.Message.Id);

            var result = await DB.Default.DeleteAsync<Item>(context.Message.Id);

            if (!result.IsAcknowledged)
                throw new MessageException(typeof(AuctionDeleted), "mongodb - Problem deleting auction");
        }
    }
}
