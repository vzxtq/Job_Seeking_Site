FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY AuthService/AuthService.csproj ./AuthService/
COPY JobPlatform.Shared/JobPlatform.Shared.csproj ./JobPlatform.Shared/

RUN dotnet restore ./AuthService/AuthService.csproj

COPY JobPlatform.Shared/ ./JobPlatform.Shared/
COPY AuthService/ ./AuthService/

RUN dotnet publish ./AuthService/AuthService.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /out .

ENTRYPOINT ["dotnet", "AuthService.dll"]
