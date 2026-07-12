using Microsoft.EntityFrameworkCore;
using Monart.AuctionService.Data;
using Monart.AuctionService.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AuctionDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

//app.MapControllers();
app.MapGet("/getall", async (AuctionDbContext context, CancellationToken cancellationToken) =>
{
    List<Auction> auctions = await context.Auctions.ToListAsync(cancellationToken);

    return Results.Ok(auctions);
});
try
{
    DbInitializer.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

app.Run();
