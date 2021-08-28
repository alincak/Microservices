FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Course.Shared/*.csproj Shared/Course.Shared/

COPY Services/Order/FreeCourse.API.Order.Application/*.csproj Services/Order/FreeCourse.API.Order.Application/

COPY Services/Order/FreeCourse.API.Order.Domain/*.csproj Services/Order/FreeCourse.API.Order.Domain/

COPY Services/Order/FreeCourse.API.Order.Domain.Core/*.csproj Services/Order/FreeCourse.API.Order.Domain.Core/

COPY Services/Order/FreeCourse.API.Order.Infrastructure/*.csproj Services/Order/FreeCourse.API.Order.Infrastructure/


COPY Services/Order/FreeCourse.API.Order.API/*.csproj Services/Order/FreeCourse.API.Order/
RUN dotnet restore Services/Order/FreeCourse.API.Order/*.csproj
COPY . .
RUN dotnet publish Services/Order/FreeCourse.Services.Order.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeCourse.API.Order.dll" ]