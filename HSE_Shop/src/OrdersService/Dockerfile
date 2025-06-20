FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/OrdersService/OrdersService.csproj", "src/OrdersService/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]
RUN dotnet restore "src/OrdersService/OrdersService.csproj"

COPY . .
WORKDIR "/src/src/OrdersService"
RUN dotnet build "OrdersService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrdersService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrdersService.dll"]