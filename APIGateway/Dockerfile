#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app
EXPOSE 81

# Copy csproj and restore as distinct layers
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Build runtime image
FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIGateway.dll"]
