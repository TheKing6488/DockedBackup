docker build -t dockedbackup -f Dockerfiles/Dockerfile .

docker run --rm dockedbackup

docker run -it --rm  --entrypoint /bin/bash dockedbackup