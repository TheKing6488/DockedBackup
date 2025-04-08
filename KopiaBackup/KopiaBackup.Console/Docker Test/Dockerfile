FROM archlinux:latest

RUN pacman -Syu --noconfirm

COPY ./bin/Release/net9.0/linux-x64/publish/ /app

WORKDIR /app

#ENTRYPOINT ["./dein-tool-executable"]