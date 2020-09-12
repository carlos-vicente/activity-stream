#!/bin/sh -e

echo "********************* setting required environment variables" \
    && echo "PORT: $PORT" \
    && export ASPNETCORE_URLS="http://+:$PORT" \
    && echo "ASPNETCORE_URLS:$ASPNETCORE_URLS" \
    && cd /app \
    && dotnet ./Activity.Stream.Api.dll