FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FoosballApi/FoosballApi.csproj", "FoosballApi/"]
COPY ["Database/Database.csproj", "Database/"]
RUN dotnet restore "FoosballApi/FoosballApi.csproj"
COPY . .
WORKDIR "/src/FoosballApi"
RUN dotnet build "FoosballApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoosballApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoosballApi.dll"]
