FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BFSMonitoringWebService1.csproj", "./"]
RUN dotnet restore "BFSMonitoringWebService1.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "BFSMonitoringWebService1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BFSMonitoringWebService1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BFSMonitoringWebService1.dll"]
