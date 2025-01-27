# BloodBank 🩸💉

Bem-vindo ao BloodBank, um sistema de gerenciamento de banco de sangue desenvolvido como parte de um desafio da mentoria, utilizando ASP.NET Core com padrões avançados de arquitetura para oferecer uma solução eficiente e completa para controle de doações e estoque de sangue.

## Funcionalidades Implementadas

- **Cadastro de doadores**: Validação de dados e integração com API externa para consulta de CEP (PLUS).
- **Controle de estoque de sangue**: Atualização automática após doações e alertas para níveis mínimos (PLUS).
- **Registro de doações**: Gerenciamento completo das doações realizadas.
- **Consulta de doadores**: Histórico detalhado de doações por doador.
- **Consulta de Estoque**: Quantidade total de sangue por tipo disponível.
- **Relatório**: Relatório de doações realizadas nos últimos 30 dias com informações detalhadas dos doadores.

## Tecnologias Utilizadas

- **.NET 9**
- **ASP.NET Core**  
- **Blazor WebAssembly**: Interface moderna para consumir a API.  
- **CQRS (Command Query Responsibility Segregation)**: Separação de comandos (escrita) e consultas (leitura).  
- **Fluent Validation**: Validação fluente para entradas de dados (usuários e livros).  
- **Entity Framework Core**: ORM para comunicação com o banco de dados.  

## Regras de Negócio
- Não permitir o cadastro de doadores com o mesmo e-mail.
- Menores de idade podem ser cadastrados, mas não podem doar.
- Doadores devem pesar no mínimo 50 kg.
- Mulheres podem doar apenas a cada 90 dias.
- Homens podem doar apenas a cada 60 dias.
- A quantidade de sangue doada deve estar entre 420 ml e 470 ml.

## Estrutura do Projeto

O projeto segue a Arquitetura Limpa e implementa o padrão CQRS para melhorar a organização e escalabilidade. A seguir, algumas camadas principais:

- **Application Layer**: Contém os casos de uso, incluindo comandos e consultas.
- **Domain Layer**: Modelos de domínio e regras de negócios.
- **Infrastructure Layer**: Acesso a dados e implementação de repositórios.

## Pré-requisitos

Antes de começar, você vai precisar ter instalado:

- [.NET 9](https://dotnet.microsoft.com/download/dotnet/9.0)
- [JetBrains Rider](https://www.jetbrains.com/rider/) (ou outra IDE de sua preferência)
- Banco de dados: SQL Server


## Como rodar o projeto

1. Clone este repositório:
   ```bash
   git clone https://github.com/iKelviin/BloodBank.git

## Exemplos de Uso da API

### 1. CRUD de Doadores
- **GET** `/api/donors` - Lista todos os doadores.
- **GET** `/api/donors/{id}` - Obtem um doador pelo seu id.
- **POST** `/api/donors` - Adiciona um novo doador.
- **PUT** `/api/donors/{id}` - Atualiza as informações de um doador.
- **DELETE** `/api/donors/{id}` - Remove um doador.

### 2. Registro de Doações
- **GET** `/api/donations` - Lista todos as doações.
- **GET** `/api/donations/by-donor/{id}` - Obtem as doações pelo id de um doador.
- **POST** `/api/donations` - Realiza uma nova doação.

### 3. Registro de Estoque
- **GET** `/api/stocks` - Lista todos os sangues disponíveis no estoque.


## Agradecimentos

Este projeto faz parte da minha jornada de aprendizado em ASP.NET Core, .NET 9 e Blazor. Um agradecimento especial ao professor Luis Felipe e à equipe da **Next Wave Education** pela mentoria.
