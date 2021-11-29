FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# RUN apt install mariadb-server
# RUN <cmd>
# ARG <name>

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
