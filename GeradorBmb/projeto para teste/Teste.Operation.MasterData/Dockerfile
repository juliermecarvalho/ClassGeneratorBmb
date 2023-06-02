FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Bmb.Teste.Operation.MasterData.Api/Bmb.Teste.Operation.MasterData.Api.csproj", "Bmb.Teste.Operation.MasterData.Api/"]
COPY ["src/Bmb.Teste.Operation.MasterData.Domain/Bmb.Teste.Operation.MasterData.Domain.csproj", "Bmb.Teste.Operation.MasterData.Domain/"]
COPY ["src/Bmb.Teste.Operation.MasterData.Infra.Data/Bmb.Teste.Operation.MasterData.Infra.Data.csproj", "Bmb.Teste.Operation.MasterData.Infra.Data/"]
RUN dotnet restore "Bmb.Teste.Operation.MasterData.Api/Bmb.Teste.Operation.MasterData.Api.csproj"
COPY . .
WORKDIR "Bmb.Teste.Operation.MasterData.Api"
RUN dotnet build "Bmb.Teste.Operation.MasterData.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bmb.Teste.Operation.MasterData.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
COPY --from=publish --chown=1000730000:1000730000 /app/publish .
ENTRYPOINT ["dotnet", "Bmb.Teste.Operation.MasterData.Api.dll"]