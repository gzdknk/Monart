FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY ["Monart.slnx","./"]
COPY ["src/Monart.SearchService/Monart.SearchService.csproj","./src/Monart.SearchService/"]
COPY ["src/Contracts/Contracts.csproj","./src/Contracts/"]

RUN dotnet restore "./src/Monart.SearchService/Monart.SearchService.csproj"

COPY . . 
RUN dotnet publish "./src/Monart.SearchService/Monart.SearchService.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Monart.SearchService.dll"]