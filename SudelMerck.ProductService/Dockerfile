#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SudelMerck.ProductService/SudelMerck.ProductService.csproj", "SudelMerck.ProductService/"]
RUN dotnet restore "SudelMerck.ProductService/SudelMerck.ProductService.csproj"
COPY . .
WORKDIR "/src/SudelMerck.ProductService"
RUN dotnet build "SudelMerck.ProductService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SudelMerck.ProductService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SudelMerck.ProductService.dll"]