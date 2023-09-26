FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Shortener/Shortener.csproj", "Shortener/"]
COPY ["Database/Database.csproj", "Database/"]
RUN dotnet restore "Shortener/Shortener.csproj"
COPY . .
WORKDIR "/src/Shortener"
RUN dotnet build "Shortener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shortener.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shortener.dll"]
