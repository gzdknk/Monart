FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY ["Monart.slnx","./"]
COPY ["src/Monart.GatewayService/Monart.GatewayService.csproj","./src/Monart.GatewayService/"]

RUN dotnet restore "./src/Monart.GatewayService/Monart.GatewayService.csproj"

COPY . . 
RUN dotnet publish "./src/Monart.GatewayService/Monart.GatewayService.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Monart.GatewayService.dll"]