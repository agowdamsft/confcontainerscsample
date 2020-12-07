FROM scone/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["NetCoreWebApp/NetCoreWebApp.csproj", "NetCoreWebApp/"]

RUN dotnet restore "NetCoreWebApp/NetCoreWebApp.csproj"
COPY . .
WORKDIR "/src/NetCoreWebApp"
RUN dotnet build "NetCoreWebApp.csproj" -c Release -o /app/build

ENV STORAGE_ENDPOINT="DefaultEndpointsProtocol=https;AccountName=amgowdademostorage;AccountKey=FfXAMlBq5Kxzc6TNPyCB8P3QJYuPmRT8Xwj18gXZH3R5No0jbKRpPfDw+DGyexuBKfjFLo5ZW4ElMZmWW8itzA==;EndpointSuffix=core.windows.net" \
    STORAGE_TABLE_NAME="homeloansubmissions"

FROM build AS publish
RUN dotnet publish "NetCoreWebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreWebApp.dll"]