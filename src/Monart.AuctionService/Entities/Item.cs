using System.ComponentModel.DataAnnotations.Schema;

namespace Monart.AuctionService.Entities
{
    [Table("Items")]
    public class Item
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Dimensions { get; set; } = string.Empty;
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        //nav properties
        public Guid AuctionId { get; set; }
        public Auction Auction { get; set; } = null!;
    }
}
