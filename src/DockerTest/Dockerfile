FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

RUN apt update &&  apt -y upgrade
RUN apt install -y clang 
WORKDIR /app

COPY . ./

RUN cd ./DockedBackup
RUN dotnet publish -c Release -r linux-x64 -p:PublishAot=true -p:PublishTrimmed=true --self-contained true -o /app/publish


#  FROM ubuntu:latest
FROM ubuntu:latest

# 1. DBus installieren
RUN apt-get update \
    && DEBIAN_FRONTEND=noninteractive apt-get install -y dbus \
    && rm -rf /var/lib/apt/lists/*

CMD ["dbus-daemon", "--system", "--nofork", "--nopidfile"]

# FROM greyltc/archlinux-aur:latest

# RUN pacman -Syu --noconfirm
# RUN pacman -S --noconfirm rclone
# RUN aur-install aur-kopia

WORKDIR /app

COPY --from=build /app/publish .

COPY DockerTest/entrypoint.sh /usr/local/bin/entrypoint.sh
RUN chmod +x /usr/local/bin/entrypoint.sh

ENTRYPOINT ["/usr/local/bin/entrypoint.sh"]

