using Microsoft.EntityFrameworkCore;
using Monart.AuctionService.Entities;

namespace Monart.AuctionService.Data
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            using var scoped = app.Services.CreateScope();

            SeedData(scoped.ServiceProvider.GetService<AuctionDbContext>());
        }

        private static void SeedData(AuctionDbContext context)
        {
            context.Database.Migrate();

            if (context.Auctions.Any())
            {
                Console.WriteLine("Already have data - no need to seed");
                return;
            }

            var autions = new List<Auction>()
            {
                new Auction
                {
                    Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                    Status = Status.Live,
                    ReservePrice = 20000,
                    Seller = "bob",
                    AuctionEnd = DateTime.UtcNow.AddDays(10),
                    Item = new Item
                    {
                        Title = "The Starry Night",
                        Artist = "Vincent van Gogh",
                        Year = 1889,
                        Category = "Painting",
                        Dimensions = "73.7 x 92.1 cm",
                        Description = "One of the most iconic Post-Impressionist paintings.",
                        ImageUrl = "https://images.unsplash.com/photo-1579783902614-a3fb3927b6a5"
                    }
                },

                // 2 The Thinker
                new Auction
                {
                    Id = Guid.Parse("c8c3ec17-01bf-49db-82aa-1ef80b833a9f"),
                    Status = Status.Live,
                    ReservePrice = 90000,
                    Seller = "alice",
                    AuctionEnd = DateTime.UtcNow.AddDays(60),
                    Item = new Item
                    {
                        Title = "The Thinker",
                        Artist = "Auguste Rodin",
                        Year = 1904,
                        Category = "Sculpture",
                        Dimensions = "186 x 98 x 145 cm",
                        Description = "Bronze sculpture representing contemplation.",
                        ImageUrl = "https://images.unsplash.com/photo-1541961017774-22349e4a1262"
                    }
                },

                // 3 The Persistence of Memory
                new Auction
                {
                    Id = Guid.Parse("bbab4d5a-8565-48b1-9450-5ac2a5c4a654"),
                    Status = Status.Live,
                    Seller = "bob",
                    AuctionEnd = DateTime.UtcNow.AddDays(4),
                    Item = new Item
                    {
                        Title = "The Persistence of Memory",
                        Artist = "Salvador Dalí",
                        Year = 1931,
                        Category = "Painting",
                        Dimensions = "24 x 33 cm",
                        Description = "Surrealist masterpiece featuring melting clocks.",
                        ImageUrl = "https://images.unsplash.com/photo-1578301978693-85fa9c0320b9"
                    }
                },

                // 4 David
                new Auction
                {
                    Id = Guid.Parse("155225c1-4448-4066-9886-6786536e05ea"),
                    Status = Status.ReserveNotMet,
                    ReservePrice = 50000,
                    Seller = "tom",
                    AuctionEnd = DateTime.UtcNow.AddDays(-10),
                    Item = new Item
                    {
                        Title = "David",
                        Artist = "Michelangelo",
                        Year = 1504,
                        Category = "Sculpture",
                        Dimensions = "517 cm",
                        Description = "Renaissance marble sculpture depicting David.",
                        ImageUrl = "https://images.unsplash.com/photo-1577083552431-6e5fd75fcfb9"
                    }
                },

                // 5 Water Lilies
                new Auction
                {
                    Id = Guid.Parse("466e4744-4dc5-4987-aae0-b621acfc5e39"),
                    Status = Status.Live,
                    ReservePrice = 20000,
                    Seller = "alice",
                    AuctionEnd = DateTime.UtcNow.AddDays(30),
                    Item = new Item
                    {
                        Title = "Water Lilies",
                        Artist = "Claude Monet",
                        Year = 1919,
                        Category = "Painting",
                        Dimensions = "200 x 180 cm",
                        Description = "Part of Monet's famous Water Lilies series.",
                        ImageUrl = "https://images.unsplash.com/photo-1460661419201-fd4cecdf8a8b"
                    }
                },

                // 6 The Kiss
                new Auction
                {
                    Id = Guid.Parse("dc1e4071-d19d-459b-b848-b5c3cd3d151f"),
                    Status = Status.Live,
                    ReservePrice = 20000,
                    Seller = "bob",
                    AuctionEnd = DateTime.UtcNow.AddDays(45),
                    Item = new Item
                    {
                        Title = "The Kiss",
                        Artist = "Gustav Klimt",
                        Year = 1908,
                        Category = "Painting",
                        Dimensions = "180 x 180 cm",
                        Description = "Golden masterpiece from Klimt's Golden Period.",
                        ImageUrl = "https://images.unsplash.com/photo-1579783901586-d88db74b4fe4"
                    }
                },

                // 7 Balloon Dog
                new Auction
                {
                    Id = Guid.Parse("47111973-d176-4feb-848d-0ea22641c31a"),
                    Status = Status.Live,
                    ReservePrice = 150000,
                    Seller = "alice",
                    AuctionEnd = DateTime.UtcNow.AddDays(13),
                    Item = new Item
                    {
                        Title = "Balloon Dog",
                        Artist = "Jeff Koons",
                        Year = 1994,
                        Category = "Contemporary Sculpture",
                        Dimensions = "307 x 363 x 114 cm",
                        Description = "Mirror-polished stainless steel sculpture.",
                        ImageUrl = "https://images.unsplash.com/photo-1561214115-f2f134cc4912"
                    }
                },

                // 8 Girl with a Pearl Earring
                new Auction
                {
                    Id = Guid.Parse("6a5011a1-fe1f-47df-9a32-b5346b289391"),
                    Status = Status.Live,
                    Seller = "bob",
                    AuctionEnd = DateTime.UtcNow.AddDays(19),
                    Item = new Item
                    {
                        Title = "Girl with a Pearl Earring",
                        Artist = "Johannes Vermeer",
                        Year = 1665,
                        Category = "Painting",
                        Dimensions = "44.5 x 39 cm",
                        Description = "Often referred to as the 'Mona Lisa of the North'.",
                        ImageUrl = "https://images.unsplash.com/photo-1579783902614-a3fb3927b6a5"
                    }
                },

                // 9 The Great Wave off Kanagawa
                new Auction
                {
                    Id = Guid.Parse("40490065-dac7-46b6-acc4-df507e0d6570"),
                    Status = Status.Live,
                    ReservePrice = 20000,
                    Seller = "tom",
                    AuctionEnd = DateTime.UtcNow.AddDays(20),
                    Item = new Item
                    {
                        Title = "The Great Wave off Kanagawa",
                        Artist = "Hokusai",
                        Year = 1831,
                        Category = "Woodblock Print",
                        Dimensions = "25.7 x 37.9 cm",
                        Description = "The most famous ukiyo-e print in history.",
                        ImageUrl = "https://images.unsplash.com/photo-1579783901586-d88db74b4fe4"
                    }
                },

                // 10 The Scream
                new Auction
                {
                    Id = Guid.Parse("3659ac24-29dd-407a-81f5-ecfe6f924b9b"),
                    Status = Status.Live,
                    ReservePrice = 20000,
                    Seller = "bob",
                    AuctionEnd = DateTime.UtcNow.AddDays(48),
                    Item = new Item
                    {
                        Title = "The Scream",
                        Artist = "Edvard Munch",
                        Year = 1893,
                        Category = "Painting",
                        Dimensions = "91 x 73.5 cm",
                        Description = "An iconic expressionist work symbolizing anxiety.",
                        ImageUrl = "https://images.unsplash.com/photo-1547891654-e66ed7ebb968"
                    }
                }
            };

            context.AddRange(autions);
            context.SaveChanges();
        }
    }
}
