FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Pinebot/Pinebot.csproj", "Pinebot/"]
RUN dotnet restore "Pinebot/Pinebot.csproj"
COPY . .
WORKDIR "/src/Pinebot"
RUN dotnet build "Pinebot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pinebot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pinebot.dll"]
