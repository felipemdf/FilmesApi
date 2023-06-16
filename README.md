# FilmesApi

Esta é uma API de exemplo desenvolvida em ASP.NET Core para gerenciar filmes. Foi criada com o propósito de aprendizado e serve como um projeto básico para entender os conceitos do ASP.NET Core e sua implementação em uma API RESTful.

## Pré-requisitos

Certifique-se de ter os seguintes pré-requisitos instalados em seu sistema:

- [.NET Core SDK](https://dotnet.microsoft.com/download) - Versão 7.0
- [Docker](https://www.docker.com/get-started) - Caso queira executar a API em um contêiner Docker

## Configuração

Siga estas etapas para configurar e executar a API em seu ambiente local:

1. Clone este repositório em sua máquina local:
```shell
git clone https://github.com/felipemdf/FilmesApi.git
```

2. Navegue até o diretório do projeto:
```shell
cd ./FilmesApi
```

3. Restaure as dependências do projeto:
```shell
dotnet restore
```

4. Execute as migrations
```shell
dotnet ef database update
```

5. Execute a API:
```shell
dotnet run
```

A API estará disponível em http://localhost:8080.

## Docker
Se você preferir executar a API em um contêiner Docker, siga estas etapas:

1. Certifique-se de ter o Docker instalado e em execução em sua máquina.

2. Navegue até o diretório do projeto.
```shell
cd ./FilmesApi
```

3. Execute o script que vai montar e executar os containers:
```shell
sh run-services.sh
```

A API estará disponível em http://localhost:80.


