FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY ["Monart.slnx","./"]
COPY ["src/Monart.IdentityService/Monart.IdentityService.csproj","./src/Monart.IdentityService/"]

RUN dotnet restore "./src/Monart.IdentityService/Monart.IdentityService.csproj"

COPY . . 
RUN dotnet publish "./src/Monart.IdentityService/Monart.IdentityService.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Monart.IdentityService.dll"]