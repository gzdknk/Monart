using Monart.AuctionService.Entities;
using System.ComponentModel.DataAnnotations;

namespace Monart.AuctionService.Dtos
{
    public class CreateAuctionDto
    {
        [Required]
        public DateTime AuctionEnd { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Dimensions { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public decimal ReservePrice { get; set; }
    }
}
