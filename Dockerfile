FROM mcr.microsoft.com/dotnet/core/sdk:3.1.402-alpine3.12 AS builder

COPY . /source
WORKDIR /source

RUN dotnet restore
RUN dotnet publish ./Activity.Stream.Api/Activity.Stream.Api.csproj --output /app --configuration Release --no-restore


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.8-alpine3.12

WORKDIR /app
COPY --from=builder /app .

ENTRYPOINT ["dotnet"]
CMD ["/app/Activity.Stream.Api.dll"]