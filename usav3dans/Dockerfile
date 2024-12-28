FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["usav3dans/usav3dans.csproj", "usav3dans/"]
RUN dotnet restore "usav3dans/usav3dans.csproj"
COPY . .
WORKDIR "/src/usav3dans"
RUN dotnet build "usav3dans.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "usav3dans.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "usav3dans.dll"]
