FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ms-api-drive/ms-api-drive.csproj", "ms-api-drive/"]
RUN dotnet restore "ms-api-drive/ms-api-drive.csproj"
COPY . .
WORKDIR "/src/ms-api-drive"
RUN dotnet build "ms-api-drive.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ms-api-drive.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ms-api-drive.dll"]
