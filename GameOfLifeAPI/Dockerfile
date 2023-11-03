#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat



FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "./GameOfLifeAPI.sln"

RUN dotnet build "./GameOfLifeAPI.sln" --no-restore

RUN dotnet test --no-restore --no-build "./GameOfLifeAPI.sln"


WORKDIR /src/GameOfLifeAPI
RUN dotnet publish "GameOfLifeKata.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "GameOfLifeKata.API.dll"]