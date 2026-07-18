using Monart.SearchService.Entities;
using Monart.SearchService.Services;
using MongoDB.Driver;
using MongoDB.Entities;
using System.Text.Json;

namespace Monart.SearchService.Data
{
    public class DbInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            await DB.InitAsync("SearchDb", MongoClientSettings
                .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Default.Index<Item>()
                .Key(x => x.Title, KeyType.Text)
                .Key(x => x.Artist, KeyType.Text)
                .Key(x => x.Category, KeyType.Text)
                .CreateAsync();

            var count = await DB.Default.CountAsync<Item>();

            using var scope = app.Services.CreateScope();

            var httpClient = scope.ServiceProvider.GetRequiredService<AuctionSvcHttpClient>();

            var items = await httpClient.GetItemsForSearchDb();

            Console.WriteLine(items.Count + " returned from the auction service");

            if(items.Count > 0) await DB.Default.SaveAsync(items);

        }
    }
}
