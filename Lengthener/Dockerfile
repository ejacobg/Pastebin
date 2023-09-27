FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ./Database ./Database
COPY ./Lengthener ./Lengthener
RUN dotnet restore "Database/Database.csproj"
RUN dotnet restore "Lengthener/Lengthener.csproj"
WORKDIR "/src/Lengthener"
RUN dotnet build "Lengthener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lengthener.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lengthener.dll"]
