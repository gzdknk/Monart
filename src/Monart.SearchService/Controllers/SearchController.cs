using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monart.SearchService.Entities;
using Monart.SearchService.RequestHelpers;
using MongoDB.Entities;

namespace Monart.SearchService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
        {
            var query = DB.Default.PagedSearch<Item>();

            query.Sort(x => x.Ascending(i => i.Title));

            if (!string.IsNullOrEmpty(searchParams.SearchTerm))
            {
                query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
            }

            switch (searchParams.OrderBy)
            {
                case "title":
                    query.Sort(x => x.Ascending(i => i.Title));
                    break;

                case "new":
                    query.Sort(x => x.Descending(i => i.CreatedAt));
                    break;

                default:
                    query.Sort(x => x.Ascending(i => i.AuctionEnd));
                    break;
            }

            switch (searchParams.FilterBy)
            {
                case "finished":
                    query.Match(x => x.AuctionEnd < DateTime.UtcNow);
                    break;

                case "endingSoon":
                    query.Match(x =>
                        x.AuctionEnd < DateTime.UtcNow.AddHours(6) &&
                        x.AuctionEnd > DateTime.UtcNow);
                    break;

                default:
                    query.Match(x => x.AuctionEnd > DateTime.UtcNow);
                    break;
            }

            if (!string.IsNullOrEmpty(searchParams.Seller))
            {
                query.Match(x => x.Seller == searchParams.Seller);
            }

            if (!string.IsNullOrEmpty(searchParams.Winner))
            {
                query.Match(x => x.Winner == searchParams.Winner);
            }

            query.PageNumber(searchParams.PageNumber);
            query.PageSize(searchParams.PageSize);

            var result = await query.ExecuteAsync();

            return Ok(new
            {
                results = result.Results,
                pageCount = result.PageCount,
                totalCount = result.TotalCount
            });

        }
    }
}
