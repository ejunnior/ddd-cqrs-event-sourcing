FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["/service/src/Finance.Api/Finance.Api.csproj", "Finance.Api/"]
COPY ["/service/src/Finance.Domain.Core/Finance.Domain.Core.csproj", "Finance.Domain.Core/"]
COPY ["/service/src/Finance.Domain/Finance.Domain.csproj", "Finance.Domain/"]
COPY ["/service/src/Finance.Infrastructure.Data/Finance.Infrastructure.Data.csproj", "Finance.Infrastructure.Data/"]
COPY ["/service/src/Finance.Infrastructure.Data.Core/Finance.Infrastructure.Data.Core.csproj", "Finance.Infrastructure.Data.Core/"]
RUN dotnet restore "Finance.Api/Finance.Api.csproj"
COPY . .
WORKDIR "/src/service/src/Finance.Api"
RUN dotnet build "Finance.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Finance.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Finance.Api.dll"]