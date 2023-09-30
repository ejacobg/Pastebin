# https://github.com/microsoft/mssql-docker/blob/master/linux/preview/examples/mssql-customize/Dockerfile
FROM mcr.microsoft.com/mssql/server:2019-latest

# Create a config directory
RUN mkdir -p /usr/config
WORKDIR /usr/config

# Bundle config source
COPY . /usr/config

# Grant permissions for to our scripts to be executable
# https://github.com/microsoft/mssql-docker/issues/541
USER root
RUN chmod +x /usr/config/entrypoint.sh
RUN chmod +x /usr/config/configure-db.sh
USER mssql

ENTRYPOINT ["./entrypoint.sh"]