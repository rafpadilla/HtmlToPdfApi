#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HtmlToPdfApi/HtmlToPdfApi.csproj", "HtmlToPdfApi/"]
RUN dotnet restore "HtmlToPdfApi/HtmlToPdfApi.csproj"
COPY . .
WORKDIR "/src/HtmlToPdfApi"
RUN dotnet build "HtmlToPdfApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HtmlToPdfApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HtmlToPdfApi.dll"]