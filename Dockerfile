FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 44347

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["./Api/DistributedServices/DistributedServices.csproj", "DistributedServices/"]
COPY ["./Api/Domain.Core/Domain.Core.csproj", "Domain.Core/"]
COPY ["./Api/Application/Application.csproj", "Application/"]
COPY ["./Api/Domain/Domain.csproj", "Domain/"]
COPY ["./Api/Infrastructure.Data/Infrastructure.Data.csproj", "Infrastructure.Data/"]
COPY ["./Api/Infrastructure.Data.Core/Infrastructure.Data.Core.csproj", "Infrastructure.Data.Core/"]
COPY ["./Api/Infrastructure.Messages/Infrastructure.Messages.csproj", "Infrastructure.Messages/"]
RUN dotnet restore "./DistributedServices/DistributedServices.csproj"
COPY . .
WORKDIR "/src/DistributedServices"
RUN dotnet build "DistributedServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DistributedServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DistributedServices.dll"]