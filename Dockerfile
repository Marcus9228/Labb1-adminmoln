# Use the base ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Install netcat for wait-for-it script
RUN apt-get update && apt-get install -y netcat

# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Labb2-webbutv/Labb2-webbutv-netAPI.csproj", "Labb2-webbutv/"]
RUN dotnet restore "Labb2-webbutv/Labb2-webbutv-netAPI.csproj"
COPY . .
WORKDIR "/src/Labb2-webbutv"
RUN dotnet build "Labb2-webbutv-netAPI.csproj" -c Release -o /app/build

# Use the build image to publish the application
FROM build AS publish
RUN dotnet publish "Labb2-webbutv-netAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY wait-for-it.sh .
RUN chmod +x wait-for-it.sh
ENTRYPOINT ["./wait-for-it.sh", "sql_server:1433", "--", "dotnet", "Labb2-webbutv-netAPI.dll"]
