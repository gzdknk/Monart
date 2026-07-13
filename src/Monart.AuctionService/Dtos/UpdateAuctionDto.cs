namespace Monart.AuctionService.Dtos
{
    public class UpdateAuctionDto
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }
        public string Category { get; set; }
        public string Dimensions { get; set; }
        public string Description { get; set; }
    }
}
