FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5044

ENV ASPNETCORE_URLS=http://+:5044

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Todo.MigrationRunner/Todo.MigrationRunner.csproj", "Todo.MigrationRunner/"]
RUN dotnet restore "Todo.MigrationRunner/Todo.MigrationRunner.csproj"
COPY . .
WORKDIR "/src/Todo.MigrationRunner"
RUN dotnet build "Todo.MigrationRunner.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Todo.MigrationRunner.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.MigrationRunner.dll"]
