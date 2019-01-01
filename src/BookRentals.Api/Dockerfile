FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/BookRentals.Api/BookRentals.Api.csproj", "src/BookRentals.Api/"]
RUN dotnet restore "src/BookRentals.Api/BookRentals.Api.csproj"
COPY . .
WORKDIR "/src/src/BookRentals.Api"
RUN dotnet build "BookRentals.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BookRentals.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BookRentals.Api.dll"]