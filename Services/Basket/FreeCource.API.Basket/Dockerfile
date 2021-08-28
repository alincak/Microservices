FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Course.Shared/*.csproj Shared/Course.Shared/
COPY Services/Basket/FreeCource.API.Basket/*.csproj Services/Basket/FreeCource.API.Basket/
RUN dotnet restore Services/Basket/FreeCource.API.Basket/*.csproj
COPY . .
RUN dotnet publish Services/Basket/FreeCource.API.Basket/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeCource.API.Basket.dll" ]