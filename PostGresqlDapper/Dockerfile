#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app
EXPOSE 80

# Copy csproj and restore as distinct layers
WORKDIR /src
COPY ["PostGresqlDapper/PostGresqlDapper.csproj", "PostGresqlDapper/"]
RUN dotnet restore "PostGresqlDapper/PostGresqlDapper.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/PostGresqlDapper"
RUN dotnet build "PostGresqlDapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PostGresqlDapper.csproj" -c Release -o /app/publish

# Build runtime image
FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostGresqlDapper.dll"]