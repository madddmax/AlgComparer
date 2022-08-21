FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AlgComparer/AlgComparer.csproj", "AlgComparer/"]
RUN dotnet restore "AlgComparer/AlgComparer.csproj"
COPY . .
WORKDIR "/src/AlgComparer"
RUN dotnet build "AlgComparer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AlgComparer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlgComparer.dll"]
