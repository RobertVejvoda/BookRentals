#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["BookRentals.Bookings.API/BookRentals.Bookings.API.csproj", "BookRentals.Bookings.API/"]
COPY ["BookRentals.Bookings.Domain/BookRentals.Bookings.Domain.csproj", "BookRentals.Bookings.Domain/"]
COPY ["BookRentals.Bookings.Infrastructure/BookRentals.Bookings.Infrastructure.csproj", "BookRentals.Bookings.Infrastructure/"]
COPY ["BookRentals.Core/BookRentals.Core.csproj", "BookRentals.Core/"]
RUN dotnet restore "BookRentals.Bookings.API/BookRentals.Bookings.API.csproj"
COPY . .
WORKDIR "/src/BookRentals.Bookings.API"
RUN dotnet build "BookRentals.Bookings.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookRentals.Bookings.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookRentals.Bookings.API.dll"]