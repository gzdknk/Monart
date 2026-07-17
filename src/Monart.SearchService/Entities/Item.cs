using MongoDB.Entities;

namespace Monart.SearchService.Entities
{
    public class Item : Entity
    {
        public decimal ReservePrice { get; set; }
        public string Seller { get; set; }
        public string Winner { get; set; }
        public decimal SoldAmount { get; set; }
        public decimal CurrentHighBid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime AuctionEnd { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }
        public string Category { get; set; }
        public string Dimensions { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
