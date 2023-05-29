FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PetHotel/PetHotel.csproj", "PetHotel/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["LoggerService/LoggerService.csproj", "LoggerService/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "PetHotel/PetHotel.csproj"
COPY . .
WORKDIR "/src/PetHotel"
RUN dotnet build "PetHotel.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PetHotel.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetHotel.dll"]
