FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY ["Monart.slnx","./"]
COPY ["src/Monart.AuctionService/Monart.AuctionService.csproj","./src/Monart.AuctionService/"]
COPY ["src/Contracts/Contracts.csproj","./src/Contracts/"]

RUN dotnet restore "./src/Monart.AuctionService/Monart.AuctionService.csproj"

COPY . . 
RUN dotnet publish "./src/Monart.AuctionService/Monart.AuctionService.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Monart.AuctionService.dll"]