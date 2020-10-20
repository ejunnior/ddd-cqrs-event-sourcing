FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["service/src/Finance.Service/Finance.Service.csproj", "Finance.Service/"]
COPY ["/service/src/Finance.Domain.Core/Finance.Domain.Core.csproj", "Finance.Domain.Core/"]
COPY ["/service/src/Finance.Domain/Finance.Domain.csproj", "Finance.Domain/"]
COPY ["/service/src/Finance.Infrastructure.Data/Finance.Infrastructure.Data.csproj", "Finance.Infrastructure.Data/"]
COPY ["/service/src/Finance.Infrastructure.Data.Core/Finance.Infrastructure.Data.Core.csproj", "Finance.Infrastructure.Data.Core/"]
RUN dotnet restore "Finance.Service/Finance.Service.csproj"
COPY . .
WORKDIR "/src/service/src/Finance.Service"
RUN dotnet build "Finance.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Finance.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Finance.Service.dll"]