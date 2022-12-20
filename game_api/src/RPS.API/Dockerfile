#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/RPS.API/RPS.API.csproj", "src/RPS.API/"]
COPY ["src/RPS.Application/RPS.Application.csproj", "src/RPS.Application/"]
COPY ["src/RPS.Core/RPS.Core.csproj", "src/RPS.Core/"]
COPY ["src/RPS.DataAccess/RPS.DataAccess.csproj", "src/RPS.DataAccess/"]
RUN dotnet restore "src/RPS.API/RPS.API.csproj"
COPY . .
WORKDIR "/src/src/RPS.API"
RUN dotnet build "RPS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RPS.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RPS.API.dll"]