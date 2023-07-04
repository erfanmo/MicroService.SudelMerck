#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SudeMerk.OrderService/SudeMerk.OrderService.csproj", "SudeMerk.OrderService/"]
RUN dotnet restore "SudeMerk.OrderService/SudeMerk.OrderService.csproj"
COPY . .
WORKDIR "/src/SudeMerk.OrderService"
RUN dotnet build "SudeMerk.OrderService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SudeMerk.OrderService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SudeMerk.OrderService.dll"]