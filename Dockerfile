FROM mcr.microsoft.com/dotnet/core/sdk:3.1.402-alpine3.12 AS builder

COPY . /source
WORKDIR /source

RUN dotnet restore
RUN dotnet publish ./Activity.Stream.Api/Activity.Stream.Api.csproj --output /app --configuration Release --no-restore


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.8-alpine3.12

COPY --from=builder /app /app
COPY  ./docker-entrypoint.sh /docker-entrypoint.sh

ENV PORT=5000

ENTRYPOINT ["/bin/sh"]
CMD ["/docker-entrypoint.sh"]