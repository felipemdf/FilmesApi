# stop previous containers
docker stop app_api app_db &&

# # remove previous containers
docker rm app_api app_db &&

# regenerate source files after recent changes
dotnet clean && dotnet build

# rebuilt the docker services by ignoring the cache
docker compose build --no-cache &&

# launch the services in background
docker compose up -d

# Verificar o status dos serviços Docker Compose
services_status=$(docker-compose ps --services --filter "status=running")

if [ -n "$services_status" ]; then
    echo "Os serviços estão em execução: $services_status"
else
    echo "Ocorreu um erro ao iniciar os serviços."
fi