FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/PaymentsService/PaymentsService.csproj", "src/PaymentsService/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]
RUN dotnet restore "src/PaymentsService/PaymentsService.csproj"

COPY . .
WORKDIR "/src/src/PaymentsService"
RUN dotnet build "PaymentsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentsService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentsService.dll"]