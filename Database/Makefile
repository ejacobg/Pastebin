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