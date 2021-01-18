#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Monitor.WebApi/Monitor.WebApi.csproj", "Monitor.WebApi/"]
COPY ["Monitor.Infrastructure/Monitor.Infrastructure.csproj", "Monitor.Infrastructure/"]
COPY ["Monitor.Domain/Monitor.Domain.csproj", "Monitor.Domain/"]
COPY ["Monitor.Common/Monitor.Common.csproj", "Monitor.Common/"]
COPY ["Monitor.IdentityServer/Monitor.IdentityServer.csproj", "Monitor.IdentityServer/"]
COPY ["Monitor.Business/Monitor.Business.csproj", "Monitor.Business/"]
RUN dotnet restore "Monitor.WebApi/Monitor.WebApi.csproj"
COPY . .
WORKDIR "/src/Monitor.WebApi"
RUN dotnet build "Monitor.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Monitor.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Monitor.WebApi.dll"]