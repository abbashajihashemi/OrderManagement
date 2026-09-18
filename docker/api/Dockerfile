# ======================
# Stage 1: Build
# ======================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copying project files for better restoration (cache layer)
COPY ["src/OrderManagement.Api/OrderManagement.Api.csproj", "src/OrderManagement.Api/"]
COPY ["src/OrderManagement.Application/OrderManagement.Application.csproj", "src/OrderManagement.Application/"]
COPY ["src/OrderManagement.Domain/OrderManagement.Domain.csproj", "src/OrderManagement.Domain/"]
COPY ["src/OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj", "src/OrderManagement.Infrastructure/"]

RUN dotnet restore "src/OrderManagement.Api/OrderManagement.Api.csproj"

# Copy other sources
COPY . .

WORKDIR "/src/src/OrderManagement.Api"
RUN dotnet build "OrderManagement.Api.csproj" -c Release -o /app/build

# ======================
# Stage 2: Publish
# ======================
FROM build AS publish
RUN dotnet publish "OrderManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ======================
# Stage 3: Runtime
# ======================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# for non-root environment (better)
RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8010
EXPOSE 8010

ENTRYPOINT ["dotnet", "OrderManagement.Api.dll"]