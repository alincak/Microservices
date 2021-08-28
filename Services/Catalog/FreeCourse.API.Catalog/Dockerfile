FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Course.Shared/*.csproj Shared/Course.Shared/
COPY Services\Catalog\FreeCourse.API.Catalog/*.csproj Services\Catalog\FreeCourse.API.Catalog/
RUN dotnet restore Services\Catalog\FreeCourse.API.Catalog/*.csproj
COPY . .
RUN dotnet publish Services\Catalog\FreeCourse.API.Catalog/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeCourse.API.Catalog.dll" ]