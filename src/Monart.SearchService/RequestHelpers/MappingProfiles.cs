using AutoMapper;
using Contracts.Auction;
using Monart.SearchService.Entities;

namespace Monart.SearchService.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AuctionCreated, Item>();
            CreateMap<AuctionUpdated, Item>();
            CreateMap<AuctionDeleted, Item>();
        }
    }
}
