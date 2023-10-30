#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["../GameOfLifeAPI/GameOfLifeKata.API.csproj", "GameOfLifeAPI/"]
COPY ["../GameOfLife.Business/GameOfLifeKata.Business.csproj", "GameOfLife.Business/"]
COPY ["../GameOfLifeKata.Infrastructure/GameOfLifeKata.Infrastructure.csproj", "GameOfLifeKata.Infrastructure/"]
RUN dotnet restore "GameOfLifeAPI/GameOfLifeKata.API.csproj"
COPY . .
WORKDIR "/src/GameOfLifeAPI"
RUN dotnet build "GameOfLifeKata.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameOfLifeKata.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameOfLifeKata.API.dll"]