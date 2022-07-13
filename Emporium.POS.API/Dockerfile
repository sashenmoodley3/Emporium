FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Emporium.POS.API/Emporium.POS.API.csproj", "Emporium.POS.API/"]
RUN dotnet restore "Emporium.POS.API/Emporium.POS.API.csproj"
COPY . .
WORKDIR "/src/Emporium.POS.API"
RUN dotnet build "Emporium.POS.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Emporium.POS.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Emporium.POS.API.dll"]