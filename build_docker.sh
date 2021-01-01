set -e

docker build --no-cache -t drkno/Your2020 .
docker push drkno/Your2020
