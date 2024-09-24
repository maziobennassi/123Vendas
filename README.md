# Resumo do projeto
API de vendas para a 123Vendas utilizando arquitetura de microsserviços, clean architecture e DDD, com todas as boas práticas de desenvolvimento e clean code.

## 🔨 Passo a passo para execução do projeto

Primeiro passso é clonar o repositório 

```sh
$ git clone https://github.com/maziobennassi/123Vendas.git
$ cd 123Vendas
```
Logo após utilizar o seguinte comando:

```sh
$ dotnet restore
```

Para disponibilizar a aplicação no docker utilizar o comando:

```sh
$ docker compose up -d
```

Acessar o swagger da aplicação:
http://localhost:5274/swagger/index.html

Acessar o RabbitMQ Management:
http://localhost:15672/#/


## ✔️ Tecnologias utilizadas

- ``.NET Core 8``
- ``Docker``
- ``SQL Server``
- ``RabbitMQ``
- ``Entity Framework``
- ``Serilog``