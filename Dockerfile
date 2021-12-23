FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 8082
ENV ASPNETCORE_URLS=http://*:8082

LABEL org.opencontainers.image.source=https://github.com/dedlocc/history-jeopardy

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HistoryJeopardy.csproj", "./"]
RUN dotnet restore "HistoryJeopardy.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "HistoryJeopardy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HistoryJeopardy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HistoryJeopardy.dll"]
