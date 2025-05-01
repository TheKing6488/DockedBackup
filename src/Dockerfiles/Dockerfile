FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

RUN apt update \
 && apt -y upgrade \
 && apt install -y clang

WORKDIR /app
COPY . ./

RUN dotnet publish \
    -c Release \
    -r linux-x64 \
    -p:PublishAot=false \
    -p:PublishTrimmed=false \
    --self-contained true \
    -o /app/publish

FROM ubuntu:latest

WORKDIR /app

COPY --from=build /app/publish .



COPY Dockerfiles/entrypoint.sh /usr/local/bin/entrypoint.sh
RUN chmod +x /usr/local/bin/entrypoint.sh

ENTRYPOINT ["/usr/local/bin/entrypoint.sh"]