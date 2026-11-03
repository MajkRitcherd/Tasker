# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files
COPY Tasker.Web/Tasker.Web.csproj Tasker.Web/
COPY Tasker.Tests/Tasker.Tests.csproj Tasker.Tests/

# Restore dependencies
RUN dotnet restore Tasker.Web/Tasker.Web.csproj

# Copy remaining source code
COPY Tasker.Web/ Tasker.Web/

# Build application
RUN dotnet build Tasker.Web/Tasker.Web.csproj -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish Tasker.Web/Tasker.Web.csproj -c Release -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

# Create non-root user for security
RUN useradd -m -u 1000 appuser
USER appuser

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasker.Web.dll"]
