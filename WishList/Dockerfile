FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["WishList.csproj", "./"]
RUN dotnet restore "WishList.csproj"
COPY . .
RUN dotnet build "WishList.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WishList.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WishList.dll"]