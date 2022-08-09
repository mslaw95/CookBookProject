FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
Expose 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CookBookBE/CookBookBE.csproj", "CookBookBE/"]
RUN dotnet restore "CookBookBE/CookBookBE.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CookBookBE/CookBookBE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CookBookBE/CookBookBE.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CookBookBE.dll"]