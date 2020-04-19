docker container stop $(docker container ls -aq)
docker container rm $(docker container ls -aq)
docker image rm $(docker image ls -aq) -f
docker-compose -f docker-compose.yml -f docker-compose.production.yml build --no-cache --force-rm --compress
docker tag $(docker image ls -f=reference="qpancapi" -q) qpanc.azurecr.io/api:latest
docker tag $(docker image ls -f=reference="qpancapp" -q) qpanc.azurecr.io/app:latest
docker push qpanc.azurecr.io/api:latest
docker push qpanc.azurecr.io/app:latest