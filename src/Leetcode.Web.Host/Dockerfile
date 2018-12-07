FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Leetcode.Web.Host/Leetcode.Web.Host.csproj", "Leetcode.Web.Host/"]
RUN dotnet restore "Leetcode.Web.Host/Leetcode.Web.Host.csproj"
COPY . .
WORKDIR "/src/Leetcode.Web.Host"
RUN dotnet build "Leetcode.Web.Host.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Leetcode.Web.Host.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Leetcode.Web.Host.dll"]