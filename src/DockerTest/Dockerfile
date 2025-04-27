FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

RUN apt update &&  apt -y upgrade
RUN apt install -y clang 
WORKDIR /app

COPY . ./
RUN dotnet restore

RUN cd ./DockedBackup
RUN dotnet publish -c Release -r linux-x64 --self-contained true -o /app/publish

FROM greyltc/archlinux-aur:latest

RUN pacman -Syu --noconfirm
RUN pacman -S --noconfirm rclone
RUN aur-install aur-kopia

WORKDIR /app

COPY --from=build /app/publish .

COPY DockerTest/entrypoint.sh /usr/local/bin/entrypoint.sh
RUN chmod +x /usr/local/bin/entrypoint.sh

ENTRYPOINT ["/usr/local/bin/entrypoint.sh"]

