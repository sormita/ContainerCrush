#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app
EXPOSE 80

# Copy csproj and restore as distinct layers
WORKDIR /src
COPY ["LoginServiceDapr/LoginServiceDapr.csproj", "LoginServiceDapr/"]
RUN dotnet restore "LoginServiceDapr/LoginServiceDapr.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/LoginServiceDapr"
RUN dotnet build "LoginServiceDapr.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoginServiceDapr.csproj" -c Release -o /app/publish

# Build runtime image
FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoginServiceDapr.dll"]