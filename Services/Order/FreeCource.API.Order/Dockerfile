FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Course.Shared/*.csproj Shared/Course.Shared/

COPY Services/Order/FreeCource.API.Order.Application/*.csproj Services/Order/FreeCource.API.Order.Application/

COPY Services/Order/FreeCource.API.Order.Domain/*.csproj Services/Order/FreeCource.API.Order.Domain/

COPY Services/Order/FreeCource.API.Order.Domain.Core/*.csproj Services/Order/FreeCource.API.Order.Domain.Core/

COPY Services/Order/FreeCource.API.Order.Infrastructure/*.csproj Services/Order/FreeCource.API.Order.Infrastructure/


COPY Services/Order/FreeCource.API.Order/*.csproj Services/Order/FreeCource.API.Order/
RUN dotnet restore Services/Order/FreeCource.API.Order/*.csproj
COPY . .
RUN dotnet publish Services/Order/FreeCource.API.Order/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeCource.API.Order.dll" ]