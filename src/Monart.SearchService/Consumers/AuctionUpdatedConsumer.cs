using AutoMapper;
using Contracts.Auction;
using MassTransit;
using Monart.SearchService.Entities;
using MongoDB.Entities;

namespace Monart.SearchService.Consumers
{
    public class AuctionUpdatedConsumer : IConsumer<AuctionUpdated>
    {
        private readonly IMapper _mapper;

        public AuctionUpdatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<AuctionUpdated> context)
        {
            Console.WriteLine("---> Consuming auction updated: " + context.Message.Id);

            var item = _mapper.Map<Item>(context.Message);

            var result = await DB.Default.Update<Item>()
                .Match(a => a.ID == context.Message.Id)
                .ModifyOnly(x => new
                {
                    x.Title,
                    x.Description,
                    x.Artist,
                    x.Category,
                    x.Year,
                    x.Dimensions
                }, item)
                .ExecuteAsync();

            if(!result.IsAcknowledged)
                throw new MessageException(typeof(AuctionUpdated),"mongodb - Problem updating auction");
        }
    }
}
