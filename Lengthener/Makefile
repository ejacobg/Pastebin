# ==================================================================================== #
# DOCKER
# ==================================================================================== #

.PHONY: docker/build
docker/build:
	docker build ../ -t lengthener -f ./Dockerfile
	# The build context needs to be the parent directory, which contains both the "Database" and "Lengthener" projects.
	# Because the build context has not been set to this directory, we have to explicitly set the Dockerfile to be used.

.PHONY: docker/run
docker/run:
	docker run --name lengthener --rm -p 5002:80 -p 5003:443 lengthener

.PHONY: docker/stop
docker/stop:
	docker stop lengthener