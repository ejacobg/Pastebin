.PHONY: script
script:
	dotnet ef migrations script -s ../Shortener -o output.sql

.PHONY: migration
migration:
	dotnet ef migrations add ${name} -s ../Shortener

.PHONY: update
update:
	dotnet ef database update -s ../Shortener

.PHONY: docker/build
docker/build:
	docker build . -t mssql-pastebin

.PHONY: docker/run
docker/run:
	docker run  -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=StrongPassw0rd' --name mssql-pastebin --rm -p 1433:1433 mssql-pastebin

.PHONY: docker/stop
docker/stop:
	docker stop mssql-pastebin