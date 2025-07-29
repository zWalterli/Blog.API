FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Blog.sln ./
COPY Blob.API/ ./Blob.API/
COPY Blog.Application/ ./Blog.Application/
COPY Blog.Data/ ./Blog.Data/
COPY Blog.Domain/ ./Blog.Domain/
RUN dotnet restore

WORKDIR /src/Blob.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Blob.API.dll"]