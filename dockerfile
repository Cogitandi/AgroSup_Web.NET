FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env
WORKDIR /app

COPY files ./
RUN dotnet restore .
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install --global dotnet-ef
RUN dotnet build
RUN dotnet-ef migrations add initialMigration -p AgroSup.Infrastructure -s AgroSup.WebApp
RUN dotnet-ef database update -p AgroSup.Infrastructure -s AgroSup.WebApp

COPY . ./
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "AgroSup.WebApp.dll"]
