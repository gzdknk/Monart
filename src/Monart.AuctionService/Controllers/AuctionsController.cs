using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts.Auction;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monart.AuctionService.Data;
using Monart.AuctionService.Dtos;
using Monart.AuctionService.Entities;

namespace Monart.AuctionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly AuctionDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuctionsController(AuctionDbContext context,IMapper mapper,IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions(string date)
        {
            var query = _context.Auctions.OrderBy(x => x.Item.Title).AsQueryable();

            if (!string.IsNullOrEmpty(date))
            {
                query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
            }

            return await query.ProjectTo<AuctionDto>(_mapper.ConfigurationProvider).ToListAsync();
             
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
        {
            var auction = _context.Auctions
                .Include(x=>x.Item)
                .FirstOrDefault(x => x.Id == id);

            if (auction == null)
                return NotFound();

            return _mapper.Map<AuctionDto>(auction);
        }
        [HttpPost]
        public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
        {
            var auction = _mapper.Map<Auction>(auctionDto);
            //TODO: add current user as seller
            auction.Seller = "test";

            _context.Auctions.Add(auction);


            var newAuction = _mapper.Map<AuctionDto>(auction);

            await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(newAuction));

            var result = await _context.SaveChangesAsync() > 0;


            if (!result) return BadRequest("Could not save changes to the DB");

            return CreatedAtAction(nameof(GetAuctionById), 
                new { auction.Id }, newAuction);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuction(Guid id,UpdateAuctionDto updateAuctionDto)
        {
            var auction = await _context.Auctions
                .Include(x => x.Item)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(auction == null) return NotFound();

            //TODO: check seller == username

            auction.Item.Title= updateAuctionDto.Title?? auction.Item.Title;
            auction.Item.Artist= updateAuctionDto.Artist?? auction.Item.Artist;
            auction.Item.Year= updateAuctionDto.Year > 0 ? updateAuctionDto.Year : auction.Item.Year;
            auction.Item.Category= updateAuctionDto.Category?? auction.Item.Category;
            auction.Item.Dimensions= updateAuctionDto.Dimensions?? auction.Item.Dimensions;
            auction.Item.Description= updateAuctionDto.Description?? auction.Item.Description;

            await _publishEndpoint.Publish(_mapper.Map<AuctionUpdated>(auction));

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest("Could not save changes to the DB");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuction(Guid id)
        {
            var auction = await _context.Auctions.FindAsync(id);

            if(auction == null) return NotFound();

            //TODO : check seller == username

            _context.Auctions.Remove(auction);

            await _publishEndpoint.Publish<AuctionDeleted>( new { Id = auction.Id.ToString() });

            var result = await _context.SaveChangesAsync() > 0;

            if(!result) return BadRequest("Could not save changes to the DB");

            return Ok();
        }
    }
}
