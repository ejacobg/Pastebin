# ==================================================================================== #
# DOCKER
# ==================================================================================== #

.PHONY: docker/build
docker/build:
	docker build ../ -t shortener -f ./Dockerfile
	# The build context needs to be the parent directory, which contains both the "Database" and "Lengthener" projects.
	# Because the build context has not been set to this directory, we have to explicitly set the Dockerfile to be used.

.PHONY: docker/run
docker/run:
	docker run --name shortener --rm -p 5000:80 -p 5001:443 shortener

.PHONY: docker/stop
docker/stop:
	docker stop shortener