FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Aspnet.Webapi/Aspnet.Webapi.csproj", "Aspnet.Webapi/"]
RUN dotnet restore "Aspnet.Webapi/Aspnet.Webapi.csproj"
COPY . .
WORKDIR "/src/Aspnet.Webapi"
RUN dotnet build "Aspnet.Webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aspnet.Webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aspnet.Webapi.dll"]
