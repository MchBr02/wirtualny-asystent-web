# Etap build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Skopiuj pliki źródłowe
COPY . .

# Publikacja aplikacji Blazor
RUN mkdir -p /app/keys
RUN dotnet publish "Client-ui.csproj" -c Release -o /app/publish

# Etap runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "Client-ui.dll"]

