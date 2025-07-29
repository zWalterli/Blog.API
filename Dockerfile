FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Blog.sln ./
COPY Blog.API/ ./Blog.API/
COPY Blog.Application/ ./Blog.Application/
COPY Blog.Data/ ./Blog.Data/
COPY Blog.Domain/ ./Blog.Domain/
COPY Blog.Test/ ./Blog.Test/
RUN dotnet restore

WORKDIR /src/Blog.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Blog.API.dll"]