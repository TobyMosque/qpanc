#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["QPANC.Api/QPANC.Api.csproj", "QPANC.Api/"]
COPY ["QPANC.Services/QPANC.Services.csproj", "QPANC.Services/"]
COPY ["QPANC.Domain/QPANC.Domain.csproj", "QPANC.Domain/"]
COPY ["QPANC.Services.Abstract/QPANC.Services.Abstract.csproj", "QPANC.Services.Abstract/"]
RUN dotnet restore "QPANC.Api/QPANC.Api.csproj"
COPY . .
WORKDIR "/src/QPANC.Api"
RUN dotnet build "QPANC.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QPANC.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QPANC.Api.dll"]
