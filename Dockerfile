FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["LokiLogs/LokiLogs.csproj", "LokiLogs/"]
RUN dotnet restore "LokiLogs/LokiLogs.csproj"
COPY . .
WORKDIR "/src/LokiLogs"
RUN dotnet build "LokiLogs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LokiLogs.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LokiLogs.dll"]