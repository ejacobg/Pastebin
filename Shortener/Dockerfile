FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ./Database ./Database
COPY ./Shortener ./Shortener
RUN dotnet restore "Database/Database.csproj"
RUN dotnet restore "Shortener/Shortener.csproj"
WORKDIR "/src/Shortener"
RUN dotnet build "Shortener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shortener.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shortener.dll"]
