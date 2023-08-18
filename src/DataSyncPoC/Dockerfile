FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DataBasseMonitor/DataBasseMonitor.csproj", "DataBasseMonitor/"]
RUN dotnet restore "DataBasseMonitor/DataBasseMonitor.csproj"
COPY . .
WORKDIR "/src/DataBasseMonitor"
RUN dotnet build "DataBasseMonitor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataBasseMonitor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataBasseMonitor.dll"]
